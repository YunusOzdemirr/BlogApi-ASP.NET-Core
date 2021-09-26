using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class Rank:EntityBase<int,int>,IEntity
    {

        public ICollection<Category> Categories { get; set; }
    }
}
