using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using FluentValidation;

namespace CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation
{
    public static class ValidationTool
    {
       public static IResult Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                IList<ValidationError> validationErrors = new List<ValidationError>();
                foreach (var error in result.Errors)
                {
                    validationErrors.Add(new ValidationError
                    {
                        PropertyName=error.PropertyName,
                        Message=error.ErrorMessage
                    });
                }
                return new Result(ResultStatus.Error, $"Bir veya birden fazla validasyon hatasına rastlandı.", validationErrors);
            }
            return new Result(ResultStatus.Success);
        }
    }
}
