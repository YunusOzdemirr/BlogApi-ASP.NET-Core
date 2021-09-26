using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators
{
    public class UserEmailActivateDtoValidator : AbstractValidator<UserEmailActivateDto>
    {
        public UserEmailActivateDtoValidator()
        {
            RuleFor(u => u.ActivationCode).NotEmpty().WithMessage("Aktivasyon Kodu alanı zorunludur.");
            RuleFor(u => u.ActivationCode).Length(6, 6).WithMessage("Aktivasyon Kodu alanı toplam 6 karakterden oluşmalıdır.");
        }
    }
}
