using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;
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
    public class ArticlePictureManager : ManagerBase, IArticlePictureService
    {
        public ArticlePictureManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(ArticlePictureAddDto articlePictureAddDto)
        {
            var articlePictureCount =await DbContext.ArticlePictures.AsNoTracking().CountAsync(ac => ac.ArticleId == articlePictureAddDto.ArticleId);
            var article = await DbContext.Articles.SingleOrDefaultAsync(a => a.Id == articlePictureAddDto.ArticleId);
            if (article == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale mevcut değil", "ArticleId"));
            }
            if (articlePictureCount == 3)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu makaleye daha fazla resim koyamazsınız.", "ArticleCount"));
            }

            var articlePicture = Mapper.Map<ArticlePicture>(articlePictureAddDto);
            articlePicture.CreatedByUserId = article.UserId;
            await DbContext.ArticlePictures.AddAsync(articlePicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Resim başarıyla eklendi", articlePicture);

        }

        public async Task<IDataResult> DeleteAsync(int articlePictureId, Guid CreatedByUserId)
        {
            var articlePicture = await DbContext.ArticlePictures.Include(a => a.Article).SingleOrDefaultAsync(ac => ac.Id == articlePictureId);
            //gerek olmayan kod
            //var article = DbContext.Articles.SingleOrDefault(a => a.Id == articlePicture.ArticleId);
            if (articlePicture != null && articlePicture.Article.CreatedByUserId == articlePicture.CreatedByUserId && articlePicture.Article != null)
            {
                articlePicture.IsActive = false;
                articlePicture.IsDeleted = true;
                articlePicture.ModifiedDate = DateTime.Now;
                await DbContext.SaveChangesAsync();
                return new DataResult(ResultStatus.Success, "Bu resim başarıyla silindi", articlePicture);
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale resmi bulunamadı", "articlePictureId"));
        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle)
        {
            IQueryable<ArticlePicture> query = DbContext.Set<ArticlePicture>();
            if (isActive.HasValue) query = query.Where(ap => ap.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(ap => ap.IsDeleted == isDeleted);
            if (includeArticle) query = query.AsNoTracking().Include(ap => ap.Article);

            pageSize = pageSize > 100 ? 100 : pageSize;
            var articlePicturesCount = await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(ap => ap.Id) : query.OrderByDescending(a => a.Id);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(ap => ap.Article.Title) : query.OrderByDescending(a => a.Article.Title);
                    break;
                default:
                    query = isAscending ? query.OrderBy(ap => ap.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success, new ArticlePictureListDto
            {
                ArticlePictures = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(ap => Mapper.Map<ArticlePictureDto>(ap)).ToListAsync(),
                TotalCount = articlePicturesCount,
                CurrentPage = currentPage,
                IsAscending = isAscending,
                PageSize = pageSize
            });
        }

        public Task<IDataResult> GetAllWithoutPagingAsync(bool? isActive, bool? isDeleted, OrderBy orderBy, bool isAscending, bool includeArticle)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult> GetByIdAsync(int articlePictureId, bool includeArticle)
        {
            IQueryable<ArticlePicture> query = DbContext.Set<ArticlePicture>();
            if (includeArticle) query = query.Include(a => a.Article);
            var articlePicture = await query.AsNoTracking().SingleOrDefaultAsync(a => a.Id == articlePictureId);
            if (articlePicture == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir resim bulunamadı", "Id"));
            }
            return new DataResult(ResultStatus.Success, articlePicture);
        }

        public async Task<IResult> HardDeleteAsync(int articlePictureId)
        {
            var articlePicture = await DbContext.ArticlePictures.SingleOrDefaultAsync(a => a.Id == articlePictureId);
            if (articlePicture != null)
            {
                DbContext.ArticlePictures.Remove(articlePicture);
                await DbContext.SaveChangesAsync();
                return new DataResult(ResultStatus.Success, "Resim başarıyla silindi", articlePicture);
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale resmi bulunmamakta", "Id"));
        }

        public async Task<IDataResult> UpdateAsync(ArticlePictureUpdateDto articlePictureUpdateDto)
        {
            var oldArticlePicture = await DbContext.ArticlePictures.Include(ap => ap.Article).SingleOrDefaultAsync(ap => ap.Id == articlePictureUpdateDto.Id);

            if (oldArticlePicture != null && oldArticlePicture.Article.CreatedByUserId == oldArticlePicture.CreatedByUserId && oldArticlePicture.Article != null)
            {
                var newArticlePicture = Mapper.Map<ArticlePictureUpdateDto, ArticlePicture>(articlePictureUpdateDto);
                newArticlePicture.ModifiedDate = DateTime.Now;
                DbContext.ArticlePictures.Update(newArticlePicture);
                await DbContext.SaveChangesAsync();
                return new DataResult(ResultStatus.Success, "");
            }
            throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu makale resmi size ait değil", "InvalidUser"));

        }
    }
}

