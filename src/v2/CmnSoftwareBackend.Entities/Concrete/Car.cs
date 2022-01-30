using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class Car : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}