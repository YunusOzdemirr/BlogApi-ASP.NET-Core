using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.UserPictureDtos
{
    public class UserPictureListDto:DtoGetBase
    {
        public IEnumerable<UserPicture> UserPictures{ get; set; }
    }
}

