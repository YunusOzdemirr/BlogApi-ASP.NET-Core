using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IOperationClaimService
    {
        Task<IDataResult<OperationClaimListDto>> GetAllAsync(bool isActive = true, bool isDeleted = false, bool isAscending = false);
        Task<IDataResult<OperationClaimDto>> GetByIdAsync(int operationClaimId);
        Task<IDataResult<OperationClaimDto>> AddAsync(OperationClaimAddDto operationClaimAddDto);
        Task<IDataResult<OperationClaimDto>> UpdateAsync(OperationClaimUpdateDto operationClaimUpdateDto);
        Task<IDataResult<OperationClaimDto>> DeleteAsync(int operationClaimId);
        Task<IResult> HardDeleteAsync(int operationClaimId);
    }
}
