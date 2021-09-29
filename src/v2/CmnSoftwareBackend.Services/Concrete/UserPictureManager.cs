using System;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserPictureUpdateDtos;
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
    public class UserPictureManager : ManagerBase, IUserPictureService
    {
        public UserPictureManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(UserPictureAddDto userPictureAddDto)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userPictureAddDto.UserId);
            if (user is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunamadı", "userId"));
            if (user.UserPictureId != null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(),
                    new Error("Bu kullanıcın bir resmi zaten var, Resmi silin ya da güncelleştirme yapınız", "user.UserPictureId"));

            var userPicture = Mapper.Map<UserPicture>(userPictureAddDto);
            await DbContext.UserPictures.AddAsync(userPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, userPicture);
        }

        public async Task<IDataResult> DeleteAsync(int userPictureId)
        {
            var picture = await DbContext.UserPictures.SingleOrDefaultAsync(a => a.Id == userPictureId);
            if (picture is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir resim bulunamadı.", "Id"));
            picture.IsActive = false;
            picture.IsDeleted = true;
            picture.ModifiedDate = DateTime.Now;
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, picture);
        }

        public Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> GetByIdAsync(int userPictureId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> HardDeleteAsync(int userPictureId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> UpdateAsync(UserPictureUpdateDto userPictureUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

