using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserPictureDtos;
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

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeUser)
        {
            IQueryable<UserPicture> query = DbContext.Set<UserPicture>();
            if (isActive.HasValue) query = query.AsNoTracking().Where(a => a.IsActive == isActive);
            if (isDeleted.HasValue) query = query.AsNoTracking().Where(a => a.IsDeleted == isDeleted);
            if (includeUser) query = query.AsNoTracking().Include(a => a.User);

            pageSize = pageSize > 100 ? 100 : pageSize;
            var totalCount =await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success, new UserPictureListDto
            {
                // Articles = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Article>(a)).ToListAsync(),
                UserPictures =await query.Take((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<UserPicture>(a)).ToListAsync(),
                IsAscending=isAscending,
                CurrentPage=currentPage,
                PageSize=pageSize,
                TotalCount=totalCount
            }); 
          
        }

        public async Task<IDataResult> GetByIdAsync(int userPictureId)
        {
            IQueryable<UserPicture> query = DbContext.Set<UserPicture>();
            var userPicture = await query.AsNoTracking().Include(ab=>ab.User).SingleOrDefaultAsync(a => a.Id == userPictureId);
            if (userPicture is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir resim bulunamadı", "userPictureId"));

            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> HardDeleteAsync(int userPictureId)
        {
            var userPicture = DbContext.UserPictures.SingleOrDefaultAsync(a=>a.Id==userPictureId);
            if (userPicture is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir resim bulunamadı","userPictureId"));

            DbContext.Remove(userPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, userPicture);
        }

        public async Task<IDataResult> UpdateAsync(UserPictureUpdateDto userPictureUpdateDto)
        {
         
            if (!await DbContext.UserPictures.AnyAsync(a => a.Id == userPictureUpdateDto.Id))
                throw new NotFoundArgumentException(Messages.General.ValidationError(),new Error("Böyle bir resim bulunamadı","id"));

            var newUserPicture = Mapper.Map<UserPicture>(userPictureUpdateDto);
            newUserPicture.ModifiedDate = DateTime.Now;
            DbContext.UserPictures.Update(newUserPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, newUserPicture);
        }
    }
}

