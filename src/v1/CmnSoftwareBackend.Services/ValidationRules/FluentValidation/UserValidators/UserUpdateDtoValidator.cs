using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.UserValidators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(a => a.Id).NotEmpty().WithMessage("Kullanıcı Kodu alanı zorunludur.");
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Ad alanı zorunludur.");
            RuleFor(a => a.FirstName).Length(2, 50).WithMessage("Ad alanı minimum 2 karakter, maksimum 50 karakter olmalıdır.");

            RuleFor(a => a.LastName).NotEmpty().WithMessage("Soyad alanı zorunludur.");
            RuleFor(a => a.LastName).Length(2, 50).WithMessage("Soyad alanı minimum 2 karakter, maksimum 50 karakter olmalıdır.");

            RuleFor(a => a.EmailAddress).NotEmpty().WithMessage("E-Posta Adresi alanı zorunludur.");
            RuleFor(a => a.EmailAddress).Length(10, 100).WithMessage("E-Posta Adresi alanı minimum 10 karakter, maksimum 100 karakter olmalıdır.");
            RuleFor(u => u.EmailAddress).EmailAddress().WithMessage("Lütfen geçerli bir E-Posta Adresi giriniz.");
            RuleFor(u => u.ModifiedByUserId).NotNull().WithMessage("Düzenleyen Kullanıcı Alanı Zorunludur.");
            // RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası alanı zorunludur.");
            // RuleFor(a => a.PhoneNumber).Length(11, 11).WithMessage("Telefon Numarası alanı 11 karakter olmalıdır.");
            // RuleFor(b => b.PhoneNumber).Matches(@"^(0(\d{3})(\d{3})(\d{2})(\d{2}))$").WithMessage("Lütfen geçerli bir Telefon Numarası giriniz.");
            // //Age
            // RuleFor(u => u.Age).NotNull().WithMessage("Yaş alanı zorunludur.");
            // RuleFor(u => u.Age).InclusiveBetween(10, 100).WithMessage("Yaş alanı minimum 10, maksimum 100 olmalıdır.");
            //Gender
            RuleFor(u => u.Gender).NotNull().WithMessage("Yaş alanı zorunludur.");
            RuleFor(u => u.Gender).IsInEnum().WithMessage("Lütfen geçerli bir Cinsiyet değeri seçiniz.");
            RuleFor(a => a.IsActive).NotNull().WithMessage("Aktif Mi? alanı boş geçilemez.");

        }
    }
}
