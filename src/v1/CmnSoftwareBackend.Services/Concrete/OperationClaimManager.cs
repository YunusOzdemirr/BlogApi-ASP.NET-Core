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
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class OperationClaimManager:ManagerBase,IOperationClaimService
    {
       

        public OperationClaimManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult<OperationClaimDto>> AddAsync(OperationClaimAddDto operationClaimAddDto)
        {
            var result = ValidationTool.Validate(new OperationClaimAddDtoValidator(), operationClaimAddDto);
            if (result.ResultStatus==ResultStatus.Error)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    result.ValidationErrors);
            }

            var anyNameResult = await DbContext.OperationClaims.AnyAsync(r => r.Name == operationClaimAddDto.Name);
            if (anyNameResult)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(),
                    null,new List<ValidationError>
                {
                    new ValidationError
                    {
                      PropertyName ="Name",
                      Message = "Böyle bir rol Mevcut"
                    }
                });
            }

            var operationClaim = Mapper.Map<OperationClaim>(operationClaimAddDto);
            operationClaim.CreatedDate=DateTime.Now;
            operationClaim.ModifiedDate=DateTime.Now;
            await DbContext.OperationClaims.AddAsync(operationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult<OperationClaimDto>(ResultStatus.Success,
                $"{operationClaim.Name} adlı rol başarıyla eklendi", new OperationClaimDto()
                {
                    OperationClaim = operationClaim
                });
        }

        public async Task<IDataResult<OperationClaimDto>> DeleteAsync(int operationClaimId)
        {
            var operationClaim = await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimId);
            if (operationClaim==null)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>()
                    {
                        new ValidationError()
                        {
                            PropertyName = "operationClaimId",
                            Message = "Böyle bir rol bulunmamakta"
                        }
                    });
            }
            operationClaim.ModifiedDate=DateTime.Now;
            operationClaim.IsActive = false;
            operationClaim.IsDeleted = true;
            DbContext.OperationClaims.Update(operationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult<OperationClaimDto>(ResultStatus.Success,
                $"{operationClaim.Name} adlı rol silinmiştir", new OperationClaimDto()
                {
                    OperationClaim = operationClaim
                });
        }

        public async Task<IDataResult<OperationClaimListDto>> GetAllAsync(bool isActive = true, bool isDeleted = false, bool isAscending = false)
        {
            IQueryable<OperationClaim> query = DbContext.Set<OperationClaim>();
            query = isActive ? query.Where(oc => oc.IsActive) : query.Where(oc => !oc.IsActive);
            query = isDeleted ? query.Where(oc => oc.IsDeleted) : query.Where(oc => !oc.IsDeleted);
            var operationClaims = await query.AsNoTracking().ToListAsync();
            var sortedOperationClaims = isAscending
                ? operationClaims.OrderBy(oc => oc.CreatedDate).ToList()
                :operationClaims.OrderByDescending(oc=>oc.CreatedDate).ToList();
            return new DataResult<OperationClaimListDto>(ResultStatus.Success, new OperationClaimListDto
            {
                OperationClaims = sortedOperationClaims
            });

        }

        public async Task<IDataResult<OperationClaimDto>> GetByIdAsync(int operationClaimId)
        {
            var operationClaim = await DbContext.OperationClaims.AsNoTracking().SingleOrDefaultAsync(oc => oc.Id == operationClaimId);
            if (operationClaim!=null)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Success, new OperationClaimDto
                {
                    OperationClaim=operationClaim
                });
            }
            return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(), null, new List<ValidationError>()
            {
                new ValidationError
                {
                    PropertyName="operationClaimId",
                    Message="böyle bir operation claim bulunmamakta"
                }
            });
        }

        public async Task<IResult> HardDeleteAsync(int operationClaimId)
        {
            var operationClaim = await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimId);
            if (operationClaim!=null)
            {
                DbContext.OperationClaims.Remove(operationClaim);
                await DbContext.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{operationClaim.Name} adlı rol kalıcı olarak silinmiştir.");
            }
            return new Result(ResultStatus.Warning, Messages.General.ValidationError(), new List<ValidationError>()
            {
                new ValidationError()
                {
                    PropertyName = "operationClaimId",
                    Message = "Böyle bir rol bulunmamaktadır."
                }
            });
        }

        public async Task<IDataResult<OperationClaimDto>> UpdateAsync(OperationClaimUpdateDto operationClaimUpdateDto)
        {
            var result = ValidationTool.Validate(new OperationClaimUpdateDtoValidator(), operationClaimUpdateDto);
            if (result.ResultStatus==ResultStatus.Error)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    result.ValidationErrors);
            }

            var oldOperationClaim =
                await DbContext.OperationClaims.SingleOrDefaultAsync(o => o.Id == operationClaimUpdateDto.Id);
           //yer değiştirdim ifleri eğer sorun olursa alttaki if ve altındaki kod parçacığını değiştir. Ve if sorgusunu != olarak değiştir.
            if (oldOperationClaim==null)
            {
                return new DataResult<OperationClaimDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>()
                    {
                        new ValidationError()
                        {
                            PropertyName = "Id",
                            Message = "Böyle bir rol bulunmamaktadır"
                        }
                    });
            }
            var newOperationClaim =
                Mapper.Map<OperationClaimUpdateDto, OperationClaim>(operationClaimUpdateDto, oldOperationClaim);
            newOperationClaim.ModifiedDate=DateTime.Now;
            DbContext.OperationClaims.Update(newOperationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult<OperationClaimDto>(ResultStatus.Success,
                $"{newOperationClaim.Name} adlı rol eklenmiştir", new OperationClaimDto()
                {
                    OperationClaim = newOperationClaim
                });
           
        }
    }
}
