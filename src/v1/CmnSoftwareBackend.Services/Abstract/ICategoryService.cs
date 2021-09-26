using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryListDto>> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult<CategoryDto>> GetByIdAsync(int categoryId);
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId);
        Task<IResult> HardDeleteAsync(int categoryId);
    }
}
