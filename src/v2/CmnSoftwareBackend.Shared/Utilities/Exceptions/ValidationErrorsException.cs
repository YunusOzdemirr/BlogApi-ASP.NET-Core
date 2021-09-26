using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Concrete;

namespace CmnSoftwareBackend.Shared.Utilities.Exceptions
{
    public class ValidationErrorsException:Exception
    {
        public ValidationErrorsException(string message, IEnumerable<Error> validationErrors):base(message)
        {
            ValidationErrors = validationErrors;
        }
        public IEnumerable<Error> ValidationErrors{ get; set; }
    }
}

