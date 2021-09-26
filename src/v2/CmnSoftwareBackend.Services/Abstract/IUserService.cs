using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserService
    {
        Task<IDataResult> GetAllAsync(int? groupId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderBy orderBy, bool isAscending);
        Task<IDataResult> GetByIdAsync(Guid userId, bool includeOwnedGroups, bool includeGroups);
        Task<IDataResult> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);
        Task<IDataResult> GetByUserNameAsync(string userName);
        Task<IDataResult> UpdateAsync(UserUpdateDto userUpdateDto);
        Task<IResult> DeleteAsync(Guid userId, Guid modifiedByUserId);
        Task<IResult> HardDeleteAsync(Guid userId);
    }
}
