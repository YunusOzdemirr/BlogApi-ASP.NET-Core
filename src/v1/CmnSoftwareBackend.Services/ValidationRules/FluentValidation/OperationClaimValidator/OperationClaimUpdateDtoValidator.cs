using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.OperationClaimValidator
{
    public class OperationClaimUpdateDtoValidator:AbstractValidator<OperationClaimUpdateDto>
    {
        public OperationClaimUpdateDtoValidator()
        {
            RuleFor(w => w.Id).NotEmpty().WithMessage("Operasyon Kodu alanı zorunludur.");
            RuleFor(w => w.Id).GreaterThanOrEqualTo(1).WithMessage("Operasyon Kodu alanı zorunludur.");
            RuleFor(w => w.Name).NotEmpty().WithMessage("Operasyon Adı alanı zorunludur.");
            RuleFor(w => w.Name).Length(3, 100).WithMessage("Operasyon Adı alanı en az 3, en fazla 100 karakter olmalıdır.");
            RuleFor(w => w.IsActive).NotNull().WithMessage("Aktif Mi? alanı zorunludur.");

        }
    }
}