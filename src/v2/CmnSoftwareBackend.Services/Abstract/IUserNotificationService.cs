using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserNotificationService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted,bool isAscending,int currentPage,int pageSize,OrderBy orderBy,bool includeUser);
        Task<IDataResult> GetById(int notificationId);
        Task<IDataResult> GetAllNotificationByUserId(Guid userId);
        Task<IDataResult> AddAsync(UserNotificationAddDto userNotificationAddDto);
        Task<IDataResult> UpdateAsync(UserNotificationUpdateDto userNotificationUpdateDto);
        Task<IDataResult> DeleteAsync(int notificationId);
        Task<IResult> HardDeleteAsync(int notificationId);
    }
}

