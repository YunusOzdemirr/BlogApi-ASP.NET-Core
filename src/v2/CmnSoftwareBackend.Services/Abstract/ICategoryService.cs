using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int categoryId);
        Task<IDataResult> AddAsync(CategoryAddDto categoryAddDto);
        Task<IDataResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult> DeleteAsync(int categoryId);
        Task<IResult> HardDeleteAsync(int categoryId);
    }
}
