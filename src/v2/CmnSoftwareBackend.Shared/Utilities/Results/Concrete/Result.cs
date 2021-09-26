using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;

namespace CmnSoftwareBackend.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus, string message) : this(message)
        {
            ResultStatus = resultStatus;
        }
        public Result(string message)
        {
            Message = message;
        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
    }
}
