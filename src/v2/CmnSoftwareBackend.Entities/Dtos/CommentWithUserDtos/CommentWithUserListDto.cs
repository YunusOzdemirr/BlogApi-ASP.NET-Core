using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos
{
    public class CommentWithUserListDto:DtoGetBase
    {
        public IEnumerable<CommentWithUser> CommentWithUsers { get; set; }
    }
}
