using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IArticlePictureService
    {
        Task<IDataResult<ArticlePictureListDto>> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult<ArticlePictureDto>> GetByIdAsync(int articlePictureId);
        Task<IDataResult<ArticlePictureDto>> AddAsync(ArticlePictureAddDto articlePictureAddDto);
        Task<IDataResult<ArticlePictureDto>> UpdateAsync(ArticlePictureUpdateDto articlePictureUpdateDto);
        Task<IDataResult<ArticlePictureDto>> DeleteAsync(int articlePictureId);
    }
}
