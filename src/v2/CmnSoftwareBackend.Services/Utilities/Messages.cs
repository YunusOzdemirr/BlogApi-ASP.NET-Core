using System;
namespace CmnSoftwareBackend.Services.Utilities
{
    public class Messages
    {
       public static class General
        {
            public static string ValidationError()
            {
                return $"Bir veya daha fazla validasyon hatası ile karşılaşıldı";
            }
        }
    }
}
