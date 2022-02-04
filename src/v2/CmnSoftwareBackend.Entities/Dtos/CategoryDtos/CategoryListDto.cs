using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryDtos
{
    public class CategoryListDto : DtoGetBase
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
