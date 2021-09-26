using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserOperationClaimValidator
{
    public class UserOperationClaimDeleteDtoValidator:AbstractValidator<UserOperationClaimUpdateDto>
    {
        public UserOperationClaimDeleteDtoValidator()
        {
            RuleFor(uoc => uoc.UserId).NotEmpty().WithMessage("UserId alanı zorunludur");
            RuleFor(uoc => uoc.OperationClaimId).NotEmpty().WithMessage("OperationClaimId alanı zorunludur");
            RuleFor(uoc => uoc.OperationClaimId).GreaterThan(0).WithMessage("OperationClaimId 0'dan büyük olmalıdır.");
        }
    }
}