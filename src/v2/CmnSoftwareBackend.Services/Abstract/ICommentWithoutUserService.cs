using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICommentWithoutUserService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int commentWithoutUserId);
        Task<IDataResult> AddAsync(CommentWithoutUserAddDto commentWithoutUserAddDto);
        Task<IDataResult> UpdateAsync(CommentWithoutUserUpdateDto commentWithoutUserUpdateDto);
        Task<IDataResult> DeleteAsync(int commentWithoutUserId);
        Task<IResult> HardDeleteAsync(int commentWithoutUserId);
    }
}
