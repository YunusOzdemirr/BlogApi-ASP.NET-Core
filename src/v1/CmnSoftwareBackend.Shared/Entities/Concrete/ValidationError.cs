using System;
namespace CmnSoftwareBackend.Shared.Entities.Concrete
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
