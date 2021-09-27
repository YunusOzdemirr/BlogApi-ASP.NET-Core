using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos
{
    public class CommentWithoutUserListDto:DtoGetBase
    {
        public IEnumerable<CommentWithoutUser> CommentWithoutUsers { get; set; }
    }
}
