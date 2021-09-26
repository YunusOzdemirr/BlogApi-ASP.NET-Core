using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos
{
    public class CommentWithUserListDto:DtoGetBase
    {
        public IEnumerable<CommentWithUserDto> CommentWithUsers { get; set; }
    }
}
