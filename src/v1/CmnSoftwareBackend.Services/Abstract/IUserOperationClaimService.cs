using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<UserOperationClaimDto>> GetAllByUserId(Guid userId);
        Task<IDataResult<UserOperationClaimDto>> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto);
        Task<IDataResult<UserOperationClaimDto>> UpdateAsync(UserOperationClaimUpdateDto userOperationClaimUpdateDto);
        Task<IResult> DeleteAsync(UserOperationClaimDeleteDto userOperationClaimDeleteDto);
    }
}
