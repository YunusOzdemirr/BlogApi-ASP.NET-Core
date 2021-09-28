using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IArticlePictureService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy,bool includeArticle);
        Task<IDataResult> GetAllWithoutPagingAsync(bool? isActive, bool? isDeleted, OrderBy orderBy, bool isAscending,bool includeArticle);
        Task<IDataResult> GetArticlePictureByArticleId(int articleId); 
        Task<IDataResult> GetByIdAsync(int articlePictureId,bool includeArticle);
        Task<IDataResult> AddAsync(ArticlePictureAddDto articlePictureAddDto);
        Task<IDataResult> UpdateAsync(ArticlePictureUpdateDto articlePictureUpdateDto);
        Task<IDataResult> DeleteAsync(int articlePictureId, Guid CreatedByUserId);
        Task<IResult> HardDeleteAsync(int articlePictureId);
    }
}
