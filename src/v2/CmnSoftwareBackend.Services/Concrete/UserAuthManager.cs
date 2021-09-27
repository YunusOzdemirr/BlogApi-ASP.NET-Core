using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Entities.Dtos.UserTokensDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;
using System.Linq;
using CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation;
using CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Hashing;
using CmnSoftwareBackend.Shared.Utilities.Generators;
using System.Transactions;
using CmnSoftwareBackend.Entities.Dtos.EmailDtos;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class UserAuthManager : ManagerBase, IUserAuthService
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly IMailService _mailService;

        public UserAuthManager(CmnDbContext dbContext, IMapper mapper, IMailService mailService, IJwtHelper jwtHelper) : base(dbContext, mapper)
        {
            _jwtHelper = jwtHelper;
            _mailService = mailService;
        }

        public async Task<IDataResult> ActiveEmailByActivationCodeAsync(UserEmailActivateDto userEmailActivateDto)
        {
            ValidationTool.Validate(new UserEmailActivateDtoValidator(), userEmailActivateDto);

            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EmailAddress == userEmailActivateDto.EmailAddress);
            if (user != null)
            {
                if (string.Equals(userEmailActivateDto.ActivationCode, user.VerificationCode))
                {
                    user.IsEmailAddressVerified = true;
                    user.ModifiedDate = DateTime.Now;
                    var accessToken = CreateAccessToken(user);
                    user.LastLogin = DateTime.Now;
                    UserToken userToken = new UserToken
                    {
                        UserId = user.Id,
                        Token = accessToken.Token,
                        TokenExpiration = accessToken.TokenExpiration,
                        RefreshToken = accessToken.RefreshToken,
                        RefreshTokenExpiration = accessToken.RefreshTokenExpiration,
                        CreatedByUserId = user.Id,
                        CreatedDate = DateTime.Now,
                        ModifiedByUserId = null,
                        ModifiedDate = null,
                        IpAddress = userEmailActivateDto.IpAddress,
                        IsActive = true,
                        IsDeleted = false
                    };
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //DbContext.Users.Update(user);
                        await DbContext.UserTokens.AddAsync(userToken);
                        await DbContext.SaveChangesAsync();
                        transactionScope.Complete();
                    }
                    return new DataResult(ResultStatus.Success,
                       $"{user.EmailAddress} e-posta adresi başarıyla onaylanmıştır.", new UserWithTokenDto
                       {
                           User = Mapper.Map<UserDto>(user),
                           UserToken = Mapper.Map<AccessToken>(userToken)
                       });
                }
                throw new NotFoundArgumentException(Messages.General.ValidationError(),
                    new Error($"{userEmailActivateDto.ActivationCode} numaralı aktivasyon kodu doğru değildir.", "ActivationCode"));

            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error($"{userEmailActivateDto.EmailAddress} e-posta adresine ait bir kullanıcı bulunamadı",
                "EmailAddress"));
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = GetClaims(user);
            var accessToken = _jwtHelper.CreateToken(user, claims);
            return accessToken;
        }

        public async Task<IDataResult> ForgotPasswordAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Mail adresi geçerli değil", "emailAddress"));
            }
            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
            if (user != null)
            {
                //create random password
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string newPassword = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                //user update
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ModifiedDate = DateTime.Now;
                DbContext.Update(user);
                await DbContext.SaveChangesAsync();
                //send new password to email
                _mailService.SendEmaiL(new EmailSendDto
                {
                    EmailAdress = user.EmailAddress,
                    Subject = "Şifremi Unuttum",
                    Content = $"<h5>Hesabınızda Şifre Yenileme Aksiyonu!</h5><br/><h5>Yeni şifreniz: {newPassword} </h5>"
                });
                return new DataResult(ResultStatus.Success,
                    $"{user.EmailAddress} e-posta adresine yeni şifreniz gönderilmiştir.", Mapper.Map<UserDto>(user));
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir mail adresi bulunumamakta", "emailAddress"));
        }

        public IEnumerable<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in DbContext.OperationClaims
                         join userOperationClaim in DbContext.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
        }

        public async Task<IDataResult> LoginAsync(UserLoginDto userLoginDto)
        {
            ValidationTool.Validate(new UserLoginDtoValidator(), userLoginDto);

            var user = await DbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.EmailAddress == userLoginDto.EmailAddress);
            if (user == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(),
                    new Error("Lütfen E-Posta adresinizi veya Şifrenizi kontrol ediniz", "EmailAddress & Password"));
            }
            if (HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                {
                    throw new NotFoundArgumentException(Messages.General.ValidationError(),
                        new Error("Giriş  yapabilmek için hesabınızın aktif olması gereklidir", "IsActive"));
                }
                if (!user.IsEmailAddressVerified)
                {
                    throw new NotFoundArgumentException(Messages.General.ValidationError(),
                        new Error("Giriş yapabilmeniz için e-posta adresinizi doğrulamanız gerekiyor.", "IsEmailAddressVerified"));
                }
                var accessToken = CreateAccessToken(user);
                UserToken userToken = new UserToken
                {
                    UserId = user.Id,
                    Token = accessToken.Token,
                    TokenExpiration = accessToken.TokenExpiration,
                    RefreshToken = accessToken.RefreshToken,
                    RefreshTokenExpiration = accessToken.RefreshTokenExpiration,
                    CreatedByUserId = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null,
                    ModifiedByUserId = null,
                    IpAddress = userLoginDto.IpAddress,
                    IsActive = true,
                    IsDeleted = false,
                };
                user.LastLogin = DateTime.Now;
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    DbContext.Users.Update(user);
                    await DbContext.UserTokens.AddAsync(userToken);
                    await DbContext.SaveChangesAsync();
                    transactionScope.Complete();
                }
                var userDto = Mapper.Map<UserDto>(user);
                var userOperationClaims = await DbContext.OperationClaims
                    .Where(op => op.UserOperationClaims.Any(uop => uop.UserId == user.Id)).ToListAsync();
                List<string> roles = new List<string>();
                foreach (var operationClaim in userOperationClaims)
                {
                    roles.Add(operationClaim.Name);
                }
                userDto.Roles = roles;
                return new DataResult(ResultStatus.Success, $"Sayın {user.FirstName} {user.LastName} hoş geldiniz.", new UserWithTokenDto
                {
                    User = userDto,
                    UserToken = Mapper.Map<AccessToken>(userToken)
                });
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(),
                new Error("Lütfen e-posta adresinizi ve şifrenizi kontrol edin.", "EmailAddress & Password"));
        }

        public async Task<IDataResult> RefreshUsersToken(UserRefreshTokenDto userRefreshTokenDto)
        {
            var oldUserToken = await DbContext.UserTokens.FirstOrDefaultAsync(u =>
              u.Token == userRefreshTokenDto.AccessToken);
            if (oldUserToken == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("RefreshToken eksik veya hatalı.", "RefreshToken"));
            }
            if (!string.Equals(userRefreshTokenDto.RefreshToken, oldUserToken.RefreshToken))
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("RefreshToken süresi doldu.", "RefreshToken"));
            }
            if (DateTime.Now > oldUserToken.RefreshTokenExpiration)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("RefreshToken eksik veya hatalı.", "RefreshTokenExpiration"));
            }
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == oldUserToken.UserId);
            var userOperationClaims = await DbContext.OperationClaims
                .Where(oc => oc.UserOperationClaims.Any(uoc => uoc.UserId == user.Id)).ToListAsync();
            List<string> roles = new List<string>();
            foreach (var operationClaim in userOperationClaims)
            {
                roles.Add(operationClaim.Name);
            }
            var userDto = Mapper.Map<UserDto>(user);
            userDto.Roles = roles;
            var accessTokenResult = CreateAccessToken(user);
            oldUserToken.Token = accessTokenResult.Token;
            oldUserToken.TokenExpiration = accessTokenResult.TokenExpiration;
            oldUserToken.RefreshToken = accessTokenResult.RefreshToken;
            oldUserToken.RefreshTokenExpiration = accessTokenResult.RefreshTokenExpiration;
            oldUserToken.ModifiedDate = DateTime.Now;
            oldUserToken.ModifiedByUserId = user.Id;
            user.LastLogin = DateTime.Now;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DbContext.Users.Update(user);
                DbContext.UserTokens.Update(oldUserToken);
                await DbContext.SaveChangesAsync();
                transactionScope.Complete();
            }
            var userTokenDto = Mapper.Map<AccessToken>(oldUserToken);
            return new DataResult(ResultStatus.Success,
                $"{user.EmailAddress} kullanıcısına ait AccessToken yenilenmiştir.", new UserWithTokenDto
                {
                    User = userDto,
                    UserToken = userTokenDto,
                });
        }

        public async Task<IDataResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            ValidationTool.Validate(new UserRegisterDtoValidator(), userRegisterDto);

            if (await DbContext.Users.AnyAsync(u => u.EmailAddress == userRegisterDto.EmailAddress))
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu e-posta adresine kayıtlı bir kullanıcı mevcut.", "EmailAddress"));
            }
            if (await DbContext.Users.AnyAsync(u => u.UserName == userRegisterDto.UserName))
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Kullanıcı adı mevcut", "UserName"));
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = Mapper.Map<User>(userRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.VerificationCode = VerificationCodeGenerator.Generate();
            var accessToken = CreateAccessToken(user);
            UserToken userToken = new UserToken
            {
                UserId = user.Id,
                Token = accessToken.Token,
                TokenExpiration = accessToken.TokenExpiration,
                RefreshToken = accessToken.RefreshToken,
                RefreshTokenExpiration = accessToken.RefreshTokenExpiration,
                CreatedByUserId = user.Id,
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                ModifiedByUserId = null,
                IpAddress = userRegisterDto.IpAddress,
                IsActive = true,
                IsDeleted = false
            };
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = 2,
            };
            UserPicture userPicture = null;
            if (!string.IsNullOrEmpty(userRegisterDto.Base64File))
            {
                userPicture = new UserPicture
                {
                    //user classında int? UserPictureId şeklindeydi.
                    Id = (int)user.UserPictureId,
                    File = Convert.FromBase64String(userRegisterDto.Base64File),
                    UserId = user.Id,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByUserId = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedByUserId = null,
                    ModifiedDate = null
                };
            }
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await DbContext.Users.AddAsync(user);
                await DbContext.UserOperationClaims.AddAsync(userOperationClaim);
                await DbContext.UserTokens.AddAsync(userToken);
                if (userPicture is not null)
                {
                    await DbContext.UserPictures.AddAsync(userPicture);
                }
                await DbContext.SaveChangesAsync();
                transactionScope.Complete();
            };
            _mailService.SendEmaiL(new EmailSendDto
            {
                EmailAdress = user.EmailAddress,
                Subject = "E-Posta Doğrulama Kodu | Custom Modification",
                Content = $"<h5>E-Posta Doğrulama Kodunuz:{user.VerificationCode}</h5>"
            });
            return new DataResult(ResultStatus.Success,
                $"Sayın {user.FirstName} {user.LastName} uygulamamıza hoş geldiniz.", new UserWithTokenDto
                {
                    User = Mapper.Map<UserDto>(user),
                    UserToken = Mapper.Map<AccessToken>(userToken)
                });
        }

        public async Task<IDataResult> ReSendActivationCodeAsync(string emailAddress)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
            if (user == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir mail adresi bulunmamakta.", "emailAddress"));

            user.VerificationCode = VerificationCodeGenerator.Generate();
            user.ModifiedDate = DateTime.Now;
            DbContext.Update(user);
            await DbContext.SaveChangesAsync();
            _mailService.SendEmaiL(new EmailSendDto
            {
                EmailAdress = user.EmailAddress,
                Subject = "Aktivasyon Kodu",
                Content = $"<h5>Aktivasyon kodu: {user.VerificationCode}</h5>"
            });
            return new DataResult(ResultStatus.Success,
                "mail adresinize tekrar doğrulama kodu gönderilmiştir", Mapper.Map<UserDto>(user));
        }
    }
}
