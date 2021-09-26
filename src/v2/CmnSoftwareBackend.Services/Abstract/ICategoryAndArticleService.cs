using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICategoryAndArticleService
    {
        Task<IDataResult> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto);
        Task<IDataResult> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto);
    }
}
