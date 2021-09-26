using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Services.ValidationRules.FluentValidation.CategoryValidator;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class CategoryManager : ManagerBase, ICategoryService
    {
        public CategoryManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        

        public async Task<IDataResult<CategoryListDto>> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
         
            IQueryable<Category> query = DbContext.Set<Category>();
            if (isActive.HasValue) query = query.Where(c => c.IsActive == isActive.Value);
            if (isDeleted.HasValue) query = query.Where(c => c.IsDeleted == isDeleted.Value);
            pageSize = pageSize > 100 ? 100 : pageSize;
            var categoryCount = await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
                    break;
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(c => c.CreatedDate)
                        : query.OrderByDescending(c => c.CreatedDate);
                    break;
            }

            return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
            {
                Categories = await query.AsNoTracking().Skip((currentPage - 1) * pageSize).Take(pageSize)
                    .Select(o => Mapper.Map<CategoryDto>(o)).ToListAsync(),
                TotalCount = categoryCount,
                CurrentPage = currentPage,
                IsAscending = isAscending,
                PageSize = pageSize
            });
        }

        public async Task<IDataResult<CategoryDto>> GetByIdAsync(int categoryId)
        {
            IQueryable<Category> query = DbContext.Set<Category>();
            var category = await query.AsNoTracking().FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>
                    {
                        new ValidationError()
                        {
                            PropertyName = "categoryId",
                            Message = "Böyle bir kategori bulunmamakta"
                        }
                    });
            }

            var categoryDto = Mapper.Map<CategoryDto>(category);
            return new DataResult<CategoryDto>(ResultStatus.Success, categoryDto);
        }

        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto)
        {
            var result = ValidationTool.Validate(new CategoryAddDtoValidator(), categoryAddDto);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    result.ValidationErrors);
            }

            if (await DbContext.Categories.AnyAsync(c => c.Name == categoryAddDto.Name))
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = "Name",
                            Message = "Bu kategori adı zaten mevcut"
                        }
                    });
            }
            
            var category = Mapper.Map<Category>(categoryAddDto);
            await DbContext.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla eklendi",
                Mapper.Map<CategoryDto>(category));
        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var result = ValidationTool.Validate(new CategoryUpdateDtoValidator(), categoryUpdateDto);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    result.ValidationErrors);
            }

            var oldCategory = await DbContext.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryUpdateDto.Id);
            if (oldCategory == null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = "Id",
                            Message = "Böyle bir kategori bulunmamakta"
                        }
                    });
            }

            var newCategory = Mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            DbContext.Update(newCategory);
            await DbContext.SaveChangesAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success,
                $"{newCategory.Name} adlı kategori güncellenmiştir", Mapper.Map<CategoryDto>(newCategory));
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId)
        {
            var category = await DbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Warning, Messages.General.ValidationError(), null,
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = "categoryId",
                            Message = "Böyle bir kategori bulunmamakta"
                        }
                    });
            }

            category.IsActive = false;
            category.IsDeleted = true;
            DbContext.Categories.Update(category);
            await DbContext.SaveChangesAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.Name} adlı kategori silinmiştir.",
                Mapper.Map<CategoryDto>(category));
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await DbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return new Result(ResultStatus.Warning, Messages.General.ValidationError(),
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = "categoryId",
                            Message = "Böyle bir kategori bulunmamakta"
                        }
                    });
            }
            DbContext.Remove(category);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success,
                $"{category.Name} adlı kategori kalıcı olarak silinmiştir");
        }
    }
}