using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CmnSoftwareBackend.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<Error> validationErrors = new List<Error>();
                foreach (var modelStateKey in context.ModelState.Keys)
                {
                    foreach (var error in context.ModelState[modelStateKey].Errors)
                    {
                        Error modelStateError = new Error
                        {
                            PropertyName = modelStateKey,
                            Message = error.ErrorMessage
                        };
                        validationErrors.Add(modelStateError);
                    }
                }
                throw new ValidationErrorsException(Messages.General.ValidationError(), validationErrors);
                //context.Result = new BadRequestObjectResult(new ApiResult
                //{
                //    Data = null,
                //    Message = $"Bir veya daha fazla validasyon hatası ile karşılaşıldı",
                //    Detail = $"Bir veya daha fazla validasyon hatası ile karşılaşıldı",
                //    ResultStatus = ResultStatus.Warning,
                //    StatusCode = HttpStatusCode.BadRequest,
                //    ValidationErrors = modelErrors,
                //    Href = context.HttpContext.Request.GetDisplayUrl()
                //});
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
