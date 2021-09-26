using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IOperationClaimService
    {
        Task<IDataResult> GetAllAsync(bool isActive = true, bool isDeleted = false, bool isAscending = false);
        Task<IDataResult> GetByIdAsync(int operationClaimId);
        Task<IDataResult> AddAsync(OperationClaimAddDto operationClaimAddDto);
        Task<IDataResult> UpdateAsync(OperationClaimUpdateDto operationClaimUpdateDto);
        Task<IDataResult> DeleteAsync(int operationClaimId);
        Task<IResult> HardDeleteAsync(int operationClaimId);
    }
}
