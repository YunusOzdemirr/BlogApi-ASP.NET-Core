using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;

namespace CmnSoftwareBackend.Shared.Utilities.Results.Concrete
{
    public class DataResult:IDataResult
    {
        public DataResult(ResultStatus resultStatus, Object data)
        {
            ResultStatus = resultStatus;
            Data = Data;
        }
        public DataResult(ResultStatus resultStatus, string message, Object data)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
        }

        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Object Data { get; }
    }
}
