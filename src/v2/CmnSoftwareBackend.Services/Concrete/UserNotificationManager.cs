using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos;
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
    public class UserNotificationManager : ManagerBase, IUserNotificationService
    {
        public UserNotificationManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(UserNotificationAddDto userNotificationAddDto)
        {
            if (!await DbContext.Users.AsNoTracking().AnyAsync(a => a.Id == userNotificationAddDto.UserId))
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunamadı", "userId"));
            DbContext.UserNotifications.Add(Mapper.Map<UserNotification>(userNotificationAddDto));
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, userNotificationAddDto);
        }

        public async Task<IDataResult> DeleteAsync(int notificationId)
        {
            var notification = await DbContext.UserNotifications.SingleOrDefaultAsync(a => a.Id == notificationId);
            if (notification is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir bildirim bulunamadı", "notificationId"));
            notification.ModifiedDate = DateTime.Now;
            notification.IsActive = false;
            notification.IsDeleted = true;
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "bildirim başarıyla silindi", notification);
        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeUser)
        {
            IQueryable<UserNotification> query = DbContext.Set<UserNotification>();
            if (isActive.HasValue) query = query.AsNoTracking().Where(a => a.IsActive == isActive);
            if (isDeleted.HasValue) query = query.AsNoTracking().Where(a => a.IsDeleted == isDeleted);
            if (includeUser) query = query.AsNoTracking().Include(a => a.User);
            pageSize = pageSize > 100 ? 100 : pageSize;
            var notificationCount = await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success, new UserNotificationListDto
            {
                UserNotifications = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<UserNotification>(a)).ToListAsync(),
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalCount = notificationCount,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult> GetAllNotificationByUserId(Guid userId)
        {
            if (!await DbContext.Users.AsNoTracking().AnyAsync(a => a.Id == userId))
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullaıcı bulunamadı", "userId"));

            var userNotifications = DbContext.UserNotifications.AsNoTracking().Where(a => a.UserId == userId);
            return new DataResult(ResultStatus.Success, userNotifications);
        }

        public async Task<IDataResult> GetById(int notificationId)
        {
            //not neccessery
            //IQueryable<UserNotification> query = DbContext.Set<UserNotification>();
            var userNotification = await DbContext.UserNotifications.AsNoTracking().SingleOrDefaultAsync(a => a.Id == notificationId);
            if (userNotification is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir bildirim bulunamadı", "notificationId"));
            return new DataResult(ResultStatus.Success, userNotification);
        }

        public async Task<IResult> HardDeleteAsync(int notificationId)
        {
            var userNotification = await DbContext.UserNotifications.SingleOrDefaultAsync(a => a.Id == notificationId);
            if (userNotification is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir bildirim bulunamadı", "notificationId"));
            DbContext.UserNotifications.Remove(userNotification);
            return new DataResult(ResultStatus.Success, "Bildirim kalıcı olarak silindi", userNotification);
        }

        public async Task<IDataResult> UpdateAsync(UserNotificationUpdateDto userNotificationUpdateDto)
        {
            if (!await DbContext.Users.AsNoTracking().AnyAsync(a => a.Id == userNotificationUpdateDto.UserId))
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı bulunamadı", "userId"));
            var notification = await DbContext.UserNotifications.SingleOrDefaultAsync(a => a.Id == userNotificationUpdateDto.Id);
            if (notification is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir bildirim bulunamadı", "Id"));
            notification = Mapper.Map<UserNotification>(userNotificationUpdateDto);
            DbContext.UserNotifications.Update(notification);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, notification);
        }
    }
}

