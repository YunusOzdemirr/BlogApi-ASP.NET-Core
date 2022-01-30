using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CarDtos
{
    public class CarListDto : DtoGetBase
    {
        public IEnumerable<Car> Cars { get; set; }
    }
}