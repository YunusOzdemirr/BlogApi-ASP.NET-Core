using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators
{
    public class UserLoginDtoValidator:AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(u => u.EmailAddress).NotEmpty().WithMessage("E-Posta Adresi alanı zorunludur.");
            RuleFor(u => u.EmailAddress).Length(10, 100);
            //Password
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre alanı zorunludur.");
           // RuleFor(u => u.Password).Length(6, 20).WithMessage("Şifre alanı en az 6, en fazla 20 karakter olmalıdır.");
        }
    }
}
