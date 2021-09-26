using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using FluentValidation;

namespace CmnSoftwareBackend.Services.ValidationRules.FluentValidation.CategoryValidator
{
    public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}