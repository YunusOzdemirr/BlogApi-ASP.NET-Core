using System;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;

namespace CmnSoftwareBackend.Shared.Entities.Concrete
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public ResultStatus ResultStatus{ get; set; }
    }
}
