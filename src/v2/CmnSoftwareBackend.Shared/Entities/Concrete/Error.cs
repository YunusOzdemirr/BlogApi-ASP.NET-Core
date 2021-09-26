using System;
namespace CmnSoftwareBackend.Shared.Entities.Concrete
{
    public class Error
    {
        public Error(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }
        public Error()
        {

        }
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}

