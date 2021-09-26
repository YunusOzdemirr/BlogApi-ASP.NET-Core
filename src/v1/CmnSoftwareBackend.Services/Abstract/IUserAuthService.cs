using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Entities.Dtos.UserTokensDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserAuthService
    {
        IEnumerable<OperationClaim> GetClaims(User user);
        Task<IDataResult<UserWithTokenDto>> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IDataResult<UserWithTokenDto>> LoginAsync(UserLoginDto userLoginDto);
        //Task<bool> UserExistsAsync(string emailAddress);
        AccessToken CreateAccessToken(User user);

        Task<IDataResult<UserWithTokenDto>> ActiveEmailByActivationCodeAsync(UserEmailActivateDto userEmailActivateDto);
        Task<IDataResult<UserDto>> ReSendActivationCodeAsync(string emailAddress);
        Task<IDataResult<UserDto>> ForgotPasswordAsync(string emailAddress);
        Task<IDataResult<UserWithTokenDto>> RefreshUsersToken(UserRefreshTokenDto userRefreshTokenDto);

    }
}
