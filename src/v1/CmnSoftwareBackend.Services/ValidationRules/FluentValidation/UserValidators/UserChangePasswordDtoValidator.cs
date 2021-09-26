using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators
{
    public class UserChangePasswordDtoValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordDtoValidator()
        {
            RuleFor(uc => uc.Id).NotEmpty().WithMessage("Kullanıcı Kodu alanı zorunludur.");
            RuleFor(uc => uc.Password).NotEmpty().WithMessage("Şifre alanı zorunludur.");
            RuleFor(uc => uc.Password).Length(6, 25).WithMessage("Şifre alanı minimum 6 karakter, maksimum 25 karakter olmalıdır.");
            RuleFor(uc => uc.NewPassword).NotEmpty().WithMessage("Yeni Şifre alanı zorunludur.");
            RuleFor(uc => uc.NewPassword).Length(6, 25).WithMessage("Yeni Şifre alanı minimum 6 karakter, maksimum 25 karakter olmalıdır.");
        }
    }
}
