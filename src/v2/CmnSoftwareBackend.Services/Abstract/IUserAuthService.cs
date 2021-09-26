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
        Task<IDataResult> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IDataResult> LoginAsync(UserLoginDto userLoginDto);
        //Task<bool> UserExistsAsync(string emailAddress);
        AccessToken CreateAccessToken(User user);

        Task<IDataResult> ActiveEmailByActivationCodeAsync(UserEmailActivateDto userEmailActivateDto);
        Task<IDataResult> ReSendActivationCodeAsync(string emailAddress);
        Task<IDataResult> ForgotPasswordAsync(string emailAddress);
        Task<IDataResult> RefreshUsersToken(UserRefreshTokenDto userRefreshTokenDto);

    }
}
