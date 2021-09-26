using System;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Shared.Utilities.Results.Concrete
{
    public class SuccessApiResult:ApiResult
    {
        public SuccessApiResult(IResult result,string href):base(result.ResultStatus,result.Message,HttpStatusCode.OK,href)
        {
        }
    }
}

