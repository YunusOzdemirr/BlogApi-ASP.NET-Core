using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy,bool includePicture);
        Task<IDataResult> GetByIdAsync(int articleId,bool includeArticlePicture);
        Task<IDataResult> AddAsync(ArticleAddDto articleAddDto);
        Task<IDataResult> UpdateAsync(ArticleUpdateDto articleUpdateDto);
        Task<IDataResult> DeleteAsync(int articleId,Guid CreatedByUserId);
        Task<IResult> HardDeleteAsync(int articleId);
    }
}
