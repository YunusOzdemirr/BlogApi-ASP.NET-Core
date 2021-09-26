using System;
namespace CmnSoftwareBackend.Shared.Utilities.Results.Abstract
{
    public interface IDataResult : IResult
    {
        public object Data { get; } // new DataResult<Category>(ResultStatus.Success,category);
                                    // new DataResult<IList<Category>>(ResultStatus.Success, categoryList);
    }
}
