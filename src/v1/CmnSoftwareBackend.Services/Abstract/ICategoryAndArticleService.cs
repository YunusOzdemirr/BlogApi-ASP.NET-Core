using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICategoryAndArticleService
    {
        Task<IDataResult<CategoryAndArticleDto>> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto);
        Task<IDataResult<CategoryAndArticleDto>> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto);
    }
}
