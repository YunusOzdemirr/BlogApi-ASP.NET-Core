using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICategoryAndArticleService
    {
        Task<IDataResult> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto);
        Task<IDataResult> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto);
        Task<IDataResult> UpdateAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto);
        Task<IDataResult> GetAllAsync(bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle, bool includeCategory);
        Task<IDataResult> GetArticleByCategoryId(int categoryId);
        Task<IDataResult> GetCategoryByArticleId(int articleId);
    }
}
