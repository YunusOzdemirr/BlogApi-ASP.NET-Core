using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.EmailAddress).NotEmpty().WithMessage("E-Posta Adresi alanı zorunludur.");
            RuleFor(u => u.EmailAddress).Length(10, 100).WithMessage("E-Posta Adresi alanı en az 10 en fazla 100 karakter olmalıdır");
        }
    }
}
