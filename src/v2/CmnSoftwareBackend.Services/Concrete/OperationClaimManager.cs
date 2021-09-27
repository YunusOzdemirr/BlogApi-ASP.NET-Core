using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Services.ValidationRules.FluentValidation.OperationClaimValidator;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class OperationClaimManager : ManagerBase, IOperationClaimService
    {


        public OperationClaimManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(OperationClaimAddDto operationClaimAddDto)
        {
            ValidationTool.Validate(new OperationClaimAddDtoValidator(), operationClaimAddDto);

            var anyNameResult = await DbContext.OperationClaims.AnyAsync(r => r.Name == operationClaimAddDto.Name);
            if (anyNameResult)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir rol mevcut", "Exist"));

            var operationClaim = Mapper.Map<OperationClaim>(operationClaimAddDto);
            operationClaim.CreatedDate = DateTime.Now;
            await DbContext.OperationClaims.AddAsync(operationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success,
                $"{operationClaim.Name} adlı rol başarıyla eklendi", new OperationClaimDto()
                {
                    OperationClaim = operationClaim
                });
        }

        public async Task<IDataResult> DeleteAsync(int operationClaimId)
        {
            var operationClaim = await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimId);
            if (operationClaim == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Not Found", "operationClaimId"));

            operationClaim.ModifiedDate = DateTime.Now;
            operationClaim.IsActive = false;
            operationClaim.IsDeleted = true;
            //DbContext.OperationClaims.Update(operationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success,
                $"{operationClaim.Name} adlı rol silinmiştir", operationClaim);
        }

        public async Task<IDataResult> GetAllAsync(bool isActive = true, bool isDeleted = false, bool isAscending = false)
        {
            IQueryable<OperationClaim> query = DbContext.Set<OperationClaim>();
            query = isActive ? query.Where(oc => oc.IsActive) : query.Where(oc => !oc.IsActive);
            query = isDeleted ? query.Where(oc => oc.IsDeleted) : query.Where(oc => !oc.IsDeleted);
            var operationClaims = await query.AsNoTracking().ToListAsync();
            var sortedOperationClaims = isAscending
                ? operationClaims.OrderBy(oc => oc.CreatedDate).ToList()
                : operationClaims.OrderByDescending(oc => oc.CreatedDate).ToList();
            return new DataResult(ResultStatus.Success, new OperationClaimListDto
            {
                OperationClaims = sortedOperationClaims,
                TotalCount = operationClaims.Count,
            });

        }

        public async Task<IDataResult> GetByIdAsync(int operationClaimId)
        {
            IQueryable<OperationClaim> query = DbContext.Set<OperationClaim>();
            var operationClaim = await query.AsNoTracking().SingleOrDefaultAsync(oc => oc.Id == operationClaimId);
            if (operationClaim == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bulunamadı", "operationClaimId"));

            return new DataResult(ResultStatus.Success, operationClaim);
        }

        public async Task<IResult> HardDeleteAsync(int operationClaimId)
        {
            var operationClaim = await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimId);
            if (operationClaim == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bulunamadı", "operationClaimId"));

            DbContext.OperationClaims.Remove(operationClaim);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success, $"{operationClaim.Name} adlı rol kalıcı olarak silinmiştir.");
        }

        public async Task<IDataResult> UpdateAsync(OperationClaimUpdateDto operationClaimUpdateDto)
        {
            ValidationTool.Validate(new OperationClaimUpdateDtoValidator(), operationClaimUpdateDto);

            var oldOperationClaim =
                await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimUpdateDto.Id);
            if (oldOperationClaim == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bulunamadı", "Id"));

            var newOperationClaim =
                Mapper.Map<OperationClaimUpdateDto, OperationClaim>(operationClaimUpdateDto, oldOperationClaim);
            newOperationClaim.ModifiedDate = DateTime.Now;
            DbContext.OperationClaims.Update(newOperationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success,
                $"{newOperationClaim.Name} adlı rol eklenmiştir", newOperationClaim);

        }
    }
}
