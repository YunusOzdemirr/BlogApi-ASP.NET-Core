using System;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using Microsoft.EntityFrameworkCore;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using System.Linq;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class CommentWithoutUserManager : ManagerBase, ICommentWithoutUserService
    {
        public CommentWithoutUserManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(CommentWithoutUserAddDto commentWithoutUserAddDto)
        {
            var article = await DbContext.Articles.AsNoTracking().SingleOrDefaultAsync(cwy => cwy.Id == commentWithoutUserAddDto.ArticleId);
            if (article == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "ArticleId"));

            var comment = Mapper.Map<CommentWithoutUser>(commentWithoutUserAddDto);
            await DbContext.CommentWithoutUsers.AddAsync(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IDataResult> DeleteAsync(int commentWithoutUserId)
        {
            var comment = await DbContext.CommentWithoutUsers.Include(a => a.Article).AsNoTracking().SingleOrDefaultAsync(cwu => cwu.Id == commentWithoutUserId);
            if (comment == null && comment.Article == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir comment bulunamadı", "Id"));

            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.ModifiedDate = DateTime.Now;
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Başarıyla bu yorum silindi", comment);
        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle)
        {
            IQueryable<CommentWithoutUser> query = DbContext.Set<CommentWithoutUser>();
            if (isActive.HasValue) query = query.Where(a => a.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(a => a.IsDeleted == isDeleted);
            if (includeArticle) query = query.AsNoTracking().Include(a=>a.Article);

            var commentCount =await query.AsNoTracking().CountAsync();
            pageSize = pageSize > 100 ? 100 : pageSize;
            switch (orderBy)
            {
                case OrderBy.Id:
                        query = isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.UserName) : query.OrderByDescending(a => a.UserName);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success,new CommentWithoutUserListDto
            {
                //    ArticlePictures = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(ap => Mapper.Map<ArticlePictureDto>(ap)).ToListAsync(),
                CommentWithoutUsers =await query.Skip((currentPage-1)*pageSize).Take(pageSize).Select(a=>Mapper.Map<CommentWithoutUser>(a)).ToListAsync(),
                CurrentPage=currentPage,
                TotalCount=commentCount,
                PageSize=pageSize,
                IsAscending=isAscending
            });
        }

        public async Task<IDataResult> GetByIdAsync(int commentWithoutUserId, bool includeArticle)
        {
            IQueryable<CommentWithoutUser> query = DbContext.Set<CommentWithoutUser>();
            var comment = await query.AsNoTracking().SingleOrDefaultAsync(a => a.Id == commentWithoutUserId);
            if (comment == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı", "Id"));
            if (includeArticle) query = query.AsNoTracking().Include(a => a.Article);

            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IResult> HardDeleteAsync(int commentWithoutUserId)
        {
            var comment = await DbContext.CommentWithoutUsers.SingleOrDefaultAsync(cwu => cwu.Id == commentWithoutUserId);
            if (comment == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir yorum bulunamadı", "Id"));

            DbContext.CommentWithoutUsers.Remove(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Bu yorum kalıcı olarak silindi", comment);
        }

        public async Task<IDataResult> UpdateAsync(CommentWithoutUserUpdateDto commentWithoutUserUpdateDto)
        {
            var article = await DbContext.Articles.SingleOrDefaultAsync(cwu => cwu.Id == commentWithoutUserUpdateDto.ArticleId);
            if (article == null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "ArticleId"));
            if (article.Id != commentWithoutUserUpdateDto.ArticleId)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu yorum bu makaleye ait değil", "ArticleId"));

            var newComment = Mapper.Map<CommentWithoutUserUpdateDto, CommentWithoutUser>(commentWithoutUserUpdateDto);
            DbContext.CommentWithoutUsers.Update(newComment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, commentWithoutUserUpdateDto);

        }
    }
}

