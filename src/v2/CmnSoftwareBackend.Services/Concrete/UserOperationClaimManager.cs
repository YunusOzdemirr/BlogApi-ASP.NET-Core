using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Services.ValidationRules.FluentValidation.OperationClaimValidator;
using CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserOperationClaimValidator;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class UserOperationClaimManager : ManagerBase, IUserOperationClaimService
    {
        public UserOperationClaimManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> GetAllByUserId(Guid userId)
        {
            var user = await DbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                var operationClaims = await DbContext.OperationClaims
                    .Where(u => u.UserOperationClaims.Any(uop => uop.UserId == user.Id)).ToListAsync();
                return new DataResult(ResultStatus.Success, new UserOperationClaimDto()
                {
                    UserId = user.Id,
                    OperationClaims = operationClaims
                });
            }

            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunmamakta", "userId"));
        }

        public async Task<IDataResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto)
        {
            ValidationTool.Validate(new UserOperationClaimAddDtoValidator(), userOperationClaimAddDto);

            var userOperationClaim = Mapper.Map<UserOperationClaim>(userOperationClaimAddDto);
            await DbContext.UserOperationClaims.AddAsync(userOperationClaim);
            await DbContext.SaveChangesAsync();
            var operationClaims = await DbContext.OperationClaims
                .Where(oc => oc.UserOperationClaims.Any(uoc => uoc.UserId == userOperationClaimAddDto.UserId))
                .ToListAsync();
            return new DataResult(ResultStatus.Success,
                $"{userOperationClaim.User.UserName} Başarıyla yetkilendirildi", new UserOperationClaimDto()
                {
                    UserId = userOperationClaim.UserId,
                    OperationClaims = operationClaims
                });
        }

        public async Task<IDataResult> UpdateAsync(UserOperationClaimUpdateDto userOperationClaimUpdateDto)
        {
            ValidationTool.Validate(new UserOperationClaimUpdateDtoValidator(), userOperationClaimUpdateDto);


            var userOperatinoClaim = Mapper.Map<UserOperationClaim>(userOperationClaimUpdateDto);
            DbContext.UserOperationClaims.Update(userOperatinoClaim);
            await DbContext.SaveChangesAsync();
            var operationClaims = await DbContext.OperationClaims.Where(oc =>
                oc.UserOperationClaims.Any(uoc => uoc.UserId == userOperationClaimUpdateDto.UserId)).ToListAsync();
            return new DataResult(ResultStatus.Success,
                $"{userOperatinoClaim.User.UserName} yetkisi güncelleştirildi.", new UserOperationClaimDto()
                {
                    UserId = userOperatinoClaim.UserId,
                    OperationClaims = operationClaims
                });
        }

        public async Task<IResult> DeleteAsync(UserOperationClaimDeleteDto userOperationClaimDeleteDto)
        {

            ValidationTool.Validate(new UserOperationClaimDeleteDtoValidator(), userOperationClaimDeleteDto);

            var userOperationClaim = Mapper.Map<UserOperationClaim>(userOperationClaimDeleteDto);
            DbContext.UserOperationClaims.Remove(userOperationClaim);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success, $"Yetki başarıyla silinmiştir.");
        }
    }
}