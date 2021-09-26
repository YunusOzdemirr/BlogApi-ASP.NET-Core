using System;
namespace CmnSoftwareBackend.Shared.Entities.Abstract
{
    public class DtoGetBase
    {
        public virtual int CurrentPage { get; set; } = 1;
        public virtual int PageSize { get; set; } = 20;
        public virtual int TotalCount { get; set; }
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
        public virtual bool ShowPrevious => CurrentPage > 1;
        public virtual bool ShowNext => CurrentPage < TotalPages;
        public virtual bool IsAscending { get; set; } = false;
    }
}
