using System;
namespace CmnSoftwareBackend.Entities.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
