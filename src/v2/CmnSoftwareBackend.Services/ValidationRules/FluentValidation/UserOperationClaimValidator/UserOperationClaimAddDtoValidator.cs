using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserOperationClaimValidator
{
    public class UserOperationClaimAddDtoValidator:AbstractValidator<UserOperationClaimAddDto>
    {
        public UserOperationClaimAddDtoValidator()
        {
            RuleFor(uoc => uoc.UserId).NotEmpty().WithMessage("Kullanıcı Kodu alanı zorunludur");
            RuleFor(uoc => uoc.OperationClaimId).NotEmpty().WithMessage("Yetki Kodu alanı zorunludur");
            RuleFor(uoc => uoc.OperationClaimId).GreaterThan(0).WithMessage("Yetki Kodu alanı 0'dan büyük olmalıdır.");

        }
    }
}