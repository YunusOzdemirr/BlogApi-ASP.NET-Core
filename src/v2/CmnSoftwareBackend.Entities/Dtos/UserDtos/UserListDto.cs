using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserListDto:DtoGetBase
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
