using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleListDto>> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult<ArticleDto>> GetByIdAsync(int articleId);
        Task<IDataResult<ArticleDto>> AddAsync(ArticleAddDto articleAddDto);
        Task<IDataResult<ArticleDto>> UpdateAsync(ArticleUpdateDto articleUpdateDto);
        Task<IDataResult<ArticleDto>> DeleteAsync(int articleId);
        Task<IResult> HardDeleteAsync(int articleId);
    }
}
