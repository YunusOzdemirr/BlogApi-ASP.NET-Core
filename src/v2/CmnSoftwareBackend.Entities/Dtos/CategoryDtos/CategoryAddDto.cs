using System;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryDtos
{
    public class CategoryAddDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
