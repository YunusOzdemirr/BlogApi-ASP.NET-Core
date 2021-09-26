using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserListDto>> GetAllAsync(int? groupId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderBy orderBy, bool isAscending);
        Task<IDataResult<UserDto>> GetByIdAsync(Guid userId, bool includeOwnedGroups, bool includeGroups);
        Task<IDataResult<UserDto>> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);
        Task<IDataResult<bool>> GetByUserNameAsync(string userName);
        Task<IDataResult<UserDto>> UpdateAsync(UserUpdateDto userUpdateDto);
        Task<IResult> DeleteAsync(Guid userId, Guid modifiedByUserId);
        Task<IResult> HardDeleteAsync(Guid userId);
    }
}
