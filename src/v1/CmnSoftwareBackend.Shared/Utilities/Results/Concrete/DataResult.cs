using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;

namespace CmnSoftwareBackend.Shared.Utilities.Results.Concrete
{
    public class DataResult<T>:IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, T data, IEnumerable<ValidationError> validationErrors)
        {
            ResultStatus = resultStatus;
            Data = data;
            ValidationErrors = validationErrors;
        }
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, IEnumerable<ValidationError> validationErrors)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            ValidationErrors = validationErrors;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, IEnumerable<ValidationError> validationErrors, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            ValidationErrors = validationErrors;
            Exception = exception;
        }


        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public IEnumerable<ValidationError> ValidationErrors { get; }
        public T Data { get; }
    }
}
