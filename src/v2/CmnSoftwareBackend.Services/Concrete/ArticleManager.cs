using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
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
    public class ArticleManager : ManagerBase, IArticleService
    {
        public ArticleManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(ArticleAddDto articleAddDto)
        {
            var article = Mapper.Map<Article>(articleAddDto);
            if (await DbContext.Articles.AsNoTracking().AnyAsync(a => a.Title == article.Title))
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu makale başlığı daha önceden kullanılmış biraz özgün olabilirsin :)", "Title"));
            }
            DbContext.Articles.Add(article);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{articleAddDto.UserName} tarafından eklendi", article);
        }

        public async Task<IDataResult> DeleteAsync(int articleId, Guid CreatedByUserId)
        {
            var article = await DbContext.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir article mevcut değil", "Id"));
            }
            if (article.CreatedByUserId != CreatedByUserId)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Bu makale size ait değil.", "InvalidUser"));
            }
            article.IsDeleted = true;
            article.IsActive = false;
            article.ModifiedDate = DateTime.Now;
            //DbContext.Articles.Update(article);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla silinmiştir", article);
        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticlePicture)
        {
            IQueryable<Article> query = DbContext.Set<Article>();
            if (isActive.HasValue) query = query.AsNoTracking().Where(a => a.IsActive == isActive);
            if (isDeleted.HasValue) query = query.AsNoTracking().Where(a => a.IsDeleted == isDeleted);
            if (includeArticlePicture) query = query.AsNoTracking().Include(a => a.ArticlePictures);
            pageSize = pageSize > 100 ? 100 : pageSize;
            var articleCount = await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.Title) : query.OrderByDescending(a => a.Title);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
            }
            // await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(ap => Mapper.Map<ArticlePictureDto>(ap)).ToListAsync(),
            return new DataResult(ResultStatus.Success, new ArticleListDto
            {
                Articles = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Article>(a)).ToListAsync(),
                CurrentPage = currentPage,
                TotalCount = articleCount,
                PageSize = pageSize,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult> GetByIdAsync(int articleId, bool includeArticlePicture)
        {
            IQueryable<Article> query = DbContext.Set<Article>();
            var article = await query.AsNoTracking().SingleOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "Id"));
            }
            if (includeArticlePicture) query = query.AsNoTracking().Include(a => a.ArticlePictures);

            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            var article = await DbContext.Articles.AsNoTracking().SingleOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı.", "Id"));
            }
            DbContext.Articles.Remove(article);
            await DbContext.SaveChangesAsync();
            return new Result($"{article.Title} başlıklı makale kalıcı olarak silindi");
        }

        public async Task<IDataResult> UpdateAsync(ArticleUpdateDto articleUpdateDto)
        {
            var oldArticle = await DbContext.Articles.AsNoTracking().SingleOrDefaultAsync(a => a.Id == articleUpdateDto.Id);
            if (oldArticle == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunmamakta", "Id"));
            }
            var newArticle = Mapper.Map<ArticleUpdateDto, Article>(articleUpdateDto, oldArticle);
            DbContext.Articles.Update(newArticle);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Başarıyla güncelleştirildi", newArticle);
        }
    }
}

