using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos
{
    public class CommentWithoutUserListDto:DtoGetBase
    {
        public IEnumerable<CommentWithoutUserDto> CommentWithoutUsers { get; set; }
    }
}
