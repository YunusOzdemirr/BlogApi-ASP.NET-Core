using System;
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
            article.IsDeleted = true;
            article.IsActive = false;
            article.ModifiedDate = DateTime.Now;
            DbContext.Articles.Update(article);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla silinmiştir", article);
        }

        public async Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticlePicture)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult> GetByIdAsync(int articleId, bool includeArticlePicture)
        {
            var article = await DbContext.Articles.AsNoTracking().SingleOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "Id"));
            }
            return new DataResult(ResultStatus.Success, article);
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
            return new Result($"{article.Title} başlıklı makale başarıyla kalıcı olarak silindi");
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

