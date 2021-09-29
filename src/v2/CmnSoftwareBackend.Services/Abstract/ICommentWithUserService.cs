using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICommentWithUserService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle, bool includeUser);
        Task<IDataResult> GetByIdAsync(int commentWithUserId, bool includeArticle,bool includeUser);
        Task<IDataResult> GetAllCommentByUserId(Guid userId, bool includeArticle);
        Task<IDataResult> AddAsync(CommentWithUserAddDto commentWithUserAddDto);
        Task<IDataResult> UpdateAsync(CommentWithUserUpdateDto commentWithUserUpdateDto);
        Task<IDataResult> DeleteAsync(int commentWithUserId, Guid CreatedByUserId);
        Task<IResult> HardDeleteAsync(int commentWithUserId);
    }
}
