using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CarDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface ICarService
    {
        public Task<IDataResult> AddAsync(CarAddDto carAddDto);
        public Task<IDataResult> UpdateAsync(CarUpdateDto carUpdateDto);
        public Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
    }
}