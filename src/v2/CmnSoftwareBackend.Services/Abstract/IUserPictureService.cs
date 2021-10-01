using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.UserPictureDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserPictureService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy,bool includeUser);
        Task<IDataResult> GetByIdAsync(int userPictureId);
        Task<IDataResult> AddAsync(UserPictureAddDto userPictureAddDto);
        Task<IDataResult> UpdateAsync(UserPictureUpdateDto userPictureUpdateDto);
        Task<IDataResult> DeleteAsync(int userPictureId);
        Task<IDataResult> HardDeleteAsync(int userPictureId);
    }
}
