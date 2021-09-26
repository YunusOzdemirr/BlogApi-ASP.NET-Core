using System;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IRankService
    {
        Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int rankId);
        Task<IDataResult> AddAsync(Rank rank);
        Task<IDataResult> UpdateAsync(Rank rank);
        Task<IDataResult> DeleteAsync(int RankId);
        Task<IDataResult> HardDeleteAsync(int rankId);
    }
}
