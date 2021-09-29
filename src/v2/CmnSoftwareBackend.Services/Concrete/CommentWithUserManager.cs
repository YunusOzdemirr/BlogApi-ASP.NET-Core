using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos;
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
    public class CommentWithUserManager : ManagerBase, ICommentWithUserService
    {
        public CommentWithUserManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(CommentWithUserAddDto commentWithUserAddDto)
        {
            var article = await DbContext.Articles.AsNoTracking().SingleOrDefaultAsync(cwu => cwu.Id == commentWithUserAddDto.ArticleId);
            var user = await DbContext.Users.AsNoTracking().SingleOrDefaultAsync(a => a.Id == commentWithUserAddDto.UserId);
            if (article == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "ArticleId"));

            var comment = Mapper.Map<CommentWithUser>(commentWithUserAddDto);
            comment.CreatedByUserId = commentWithUserAddDto.UserId;
            comment.CreatedDate = DateTime.Now;
            await DbContext.CommentWithUsers.AddAsync(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IDataResult> DeleteAsync(int commentWithUserId, Guid CreatedByUserId)
        {
            var user = await DbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == CreatedByUserId);
            var comment = await DbContext.CommentWithUsers.SingleOrDefaultAsync(cwu => cwu.Id == commentWithUserId);
            if (comment == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı", "commentWithUserId"));
            if (user == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kullanıcı yok", "UserId"));
            if (comment.CreatedByUserId != CreatedByUserId)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu yorumu silmeye yetkiniz yok", "InvalidUserId"));

            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.ModifiedDate = DateTime.Now;
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, comment);

        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy,bool includeArticle,bool includeUser)
        {
            IQueryable<CommentWithUser> query = DbContext.Set<CommentWithUser>().AsNoTracking();
            if (isActive.HasValue) query = query.Where(a => a.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(a => a.IsDeleted == isDeleted);
            if (includeArticle) query = query.Include(a => a.Article);
            if (includeUser) query = query.Include(a => a.User);

            pageSize = pageSize > 100 ? 100 : pageSize;
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.User.UserName) : query.OrderByDescending(a => a.User.UserName);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            //await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Article>(a)).ToListAsync(),
            return new DataResult(ResultStatus.Success, new CommentWithUserListDto
            {
                CommentWithUsers = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<CommentWithUser>(a)).ToListAsync(),
                TotalCount =await query.CountAsync(),
                CurrentPage=currentPage,
                IsAscending=isAscending,
                PageSize=pageSize
            });
        }

        public async Task<IDataResult> GetByIdAsync(int commentWithUserId, bool includeArticle)
        {
            IQueryable<CommentWithUser> query = DbContext.Set<CommentWithUser>();
            var comment =await query.AsNoTracking().SingleOrDefaultAsync(a => a.Id == commentWithUserId);
            if (comment == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı", "Id"));
            if (includeArticle) query = query.AsNoTracking().Include(a => a.Article);
            return new DataResult(ResultStatus.Success,comment);
        }
        //test
        public async Task<IDataResult> GetAllCommentByUserId(Guid userId, bool includeArticle)
        {
            IQueryable<CommentWithUser> query = DbContext.Set<CommentWithUser>();
           
            if (!await DbContext.Users.AsNoTracking().AnyAsync(a=>a.Id==userId))
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("böyle bir kullanıcı bulunamadı", "userId"));
            if (includeArticle) query = query.AsNoTracking().Include(a => a.Article);

            var comment = query.AsNoTracking().Where(a => a.UserId == userId);
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IResult> HardDeleteAsync(int commentWithUserId)
        {
            var comment =await DbContext.CommentWithUsers.SingleOrDefaultAsync(a => a.Id == commentWithUserId);
            if (comment is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı","Id"));

            DbContext.CommentWithUsers.Remove(comment);
            await DbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success, "Başarıyla silindi");
        }

        public async Task<IDataResult> UpdateAsync(CommentWithUserUpdateDto commentWithUserUpdateDto)
        {
            var oldComment =await DbContext.CommentWithUsers.SingleOrDefaultAsync(a => a.Id == commentWithUserUpdateDto.Id);
            if (oldComment is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı","Id"));

            var newUser = Mapper.Map<CommentWithUserUpdateDto,CommentWithUser>(commentWithUserUpdateDto,oldComment);
            newUser.ModifiedDate = DateTime.Now;
            DbContext.CommentWithUsers.Update(newUser);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success,commentWithUserUpdateDto);
        }
    }
}

