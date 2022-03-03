using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CarDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class CarManager : ManagerBase, ICarService
    {
        public CarManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<IDataResult> AddAsync(CarAddDto carAddDto)
        {
            //var isExistCar = await DbContext.Cars.SingleOrDefaultAsync(a => a.Name == carAddDto.Name);
            //  if (isExistCar != null)
            //    throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir araba zaten mevcut", "Name"));

            var car = Mapper.Map<Car>(carAddDto);
            ///   await DbContext.Cars.AddAsync(car);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, car);
        }

        public Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable query = DbContext.Set<Car>();
            if (isActive.HasValue) query = query.where;
            return null;
        }

        public async Task<IDataResult> UpdateAsync(CarUpdateDto carUpdateDto)
        {
            // var oldCar = await DbContext.Cars.SingleOrDefaultAsync(a => a.Id == carUpdateDto.Id);
            // if (oldCar == null)
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir araba bulunmamakta", "Id"));

            //            var newCar = Mapper.Map<CarUpdateDto, Car>(carUpdateDto, oldCar);
            //newCar.ModifiedDate = DateTime.Now;
            //  DbContext.Cars.Update(newCar);
            await DbContext.SaveChangesAsync();
            return null;
        }
    }
}