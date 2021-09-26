using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserTokensDtos;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserWithTokenDto
    {
        public UserDto User { get; set; }
        //public UserToken UserToken { get; set; }
        public AccessToken UserToken { get; set; }
    }
}
