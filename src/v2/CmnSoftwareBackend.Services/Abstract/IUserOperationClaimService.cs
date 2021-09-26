using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult> GetAllByUserId(Guid userId);
        Task<IDataResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto);
        Task<IDataResult> UpdateAsync(UserOperationClaimUpdateDto userOperationClaimUpdateDto);
        Task<IResult> DeleteAsync(UserOperationClaimDeleteDto userOperationClaimDeleteDto);
    }
}
