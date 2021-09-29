using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending,
            int currentPage, int pageSize, OrderBy orderBy,bool includePicture, bool includeCommentWithoutUser, bool includeCommentWithUser);
        Task<IDataResult> GetByIdAsync(int articleId,bool includeArticlePicture,bool includeCommentWithUserId,bool includeCommentWithoutUserId);
        Task<IDataResult> GetArticleByCommentWithUserIdAsync(int commentWithUserId);
        Task<IDataResult> GetArticleByCommentWithoutUserIdAsync(int commentWithoutUserId);
        Task<IDataResult> GetArticleByUserId(Guid userId);
        Task<IDataResult> GetArticleByArticlePictureId(int articlePictureId);
        Task<IDataResult> AddAsync(ArticleAddDto articleAddDto);
        Task<IDataResult> UpdateAsync(ArticleUpdateDto articleUpdateDto);
        Task<IDataResult> DeleteAsync(int articleId,Guid CreatedByUserId);
        Task<IResult> HardDeleteAsync(int articleId);
    }
}
