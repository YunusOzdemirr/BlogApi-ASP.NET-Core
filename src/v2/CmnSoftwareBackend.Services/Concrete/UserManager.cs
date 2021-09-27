using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Hashing;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class UserManager : ManagerBase, IUserService
    {
        public UserManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userChangePasswordDto.Id);
            if (user == null)
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı  yok.", "Id"));

            if (!HashingHelper.VerifyPasswordHash(userChangePasswordDto.Password, user.PasswordHash, user.PasswordSalt))
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Şifreler eşleşmiyor.", "Password"));

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            // DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();
            var userDto = Mapper.Map<UserDto>(user);
            return new DataResult(ResultStatus.Success, Messages.General.ValidationError(), userDto);
        }

        public async Task<IResult> DeleteAsync(Guid userId, Guid modifiedByUserId)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı yok.", "Id"));

            user.ModifiedDate = DateTime.Now;
            user.IsActive = false;
            user.IsDeleted = true;
            //DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success, $"{user.LastName} adlı kullanıcı başarıyla silindi.");
        }

        public async Task<IDataResult> GetAllAsync(int? groupId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderBy orderBy, bool isAscending)
        {
            IQueryable<User> query = DbContext.Set<User>();
            if (isActive.HasValue) query = query.Where(c => c.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(c => c.IsDeleted == isDeleted);

            var usersCount = await query.AsNoTracking().CountAsync();
            pageSize = pageSize > 100 ? 100 : pageSize;
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
                default:
                    query = isAscending ? query.OrderBy(c => c.CreatedDate) : query.OrderByDescending(c => c.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success, new UserListDto
            {
                Users = await query.AsNoTracking().Skip((currentPage - 1) * pageSize).Take(pageSize).Select(u => Mapper.Map<UserDto>(u)).ToListAsync(),
                TotalCount = usersCount,
                CurrentPage = currentPage,
                IsAscending = isAscending,
                PageSize = pageSize
            });
        }

        public async Task<IDataResult> GetByIdAsync(Guid userId, bool includeOwnedGroups, bool includeGroups)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunmamaktadır.", "Id"));
            }
            return new DataResult(ResultStatus.Success, Mapper.Map<UserDto>(user));
        }

        public async Task<IDataResult> GetByUserNameAsync(string userName)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunmamaktadır.", "UserName"));
            }
            return new DataResult(ResultStatus.Success, Mapper.Map<UserDto>(user));
        }

        public async Task<IResult> HardDeleteAsync(Guid userId)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunmamaktadır.", "Id"));
            }
            DbContext.Remove(user);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success, $"{user.FirstName} adlı kullanıcı kalıcı olarak silinmiştir.");
        }

        public async Task<IDataResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var oldUser = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userUpdateDto.Id);
            if (oldUser != null)
            {
                var newUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                DbContext.Users.Update(newUser);
                await DbContext.SaveChangesAsync();
                var userDto = Mapper.Map<UserDto>(newUser);
                return new DataResult(ResultStatus.Success, $"{newUser.FirstName} adlı kullanıcı başarıyla güncelleştirildi");
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı yok.", "Id"));
        }
    }
}

