using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
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
    public class CategoryAndArticleManager : ManagerBase, ICategoryAndArticleService
    {

        public CategoryAndArticleManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto)
        {
            var category = await DbContext.Categories.AsNoTracking().Include(ab => ab.CategoryAndArticles).SingleOrDefaultAsync(a => a.Id == categoryAndArticleAddDto.CategoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kategori bulunmamakta", "categoryId"));

            var article = await DbContext.Articles.AsNoTracking().Include(ab => ab.CategoryAndArticles).SingleOrDefaultAsync(a => a.Id == categoryAndArticleAddDto.ArticleId);
            if (article is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunmamakta", "articleId"));

            var categoryAndArticle = Mapper.Map<CategoryAndArticle>(categoryAndArticleAddDto);
            category.ModifiedDate = DateTime.Now;
            category.CategoryAndArticles.Add(categoryAndArticle);
            article.CategoryAndArticles.Add(categoryAndArticle);
            DbContext.CategoryAndArticles.Add(categoryAndArticle);
            await DbContext.SaveChangesAsync();
            categoryAndArticle.Article = article;
            categoryAndArticle.Category = category;
            return new DataResult(ResultStatus.Success, categoryAndArticle);

        }

        public async Task<IDataResult> GetAllAsync(bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle, bool includeCategory)
        {
            IQueryable<CategoryAndArticle> query = DbContext.Set<CategoryAndArticle>();
            if (includeArticle) query = query.AsNoTracking().Include(a => a.Article);
            if (includeCategory) query = query.AsNoTracking().Include(a => a.Category);
            pageSize = pageSize > 100 ? 100 : pageSize;
            int count = await query.AsNoTracking().CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.ArticleId) : query.OrderByDescending(a => a.Category);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.Article.Title) : query.OrderByDescending(a => a.Category.Name);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.Article.CreatedDate) : query.OrderByDescending(a => a.Category.CreatedDate);
                    break;
            }
            return new DataResult(ResultStatus.Success, new CategoryAndArticleListDto
            {
                CategoryAndArticles = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<CategoryAndArticle>(a)).ToListAsync(),
                CurrentPage = currentPage,
                TotalCount = count,
                IsAscending = isAscending,
                PageSize = pageSize
            });
        }

        public async Task<IDataResult> GetArticleByCategoryId(int categoryId)
        {
            IQueryable<CategoryAndArticle> query = DbContext.Set<CategoryAndArticle>();
            var category = query.AsNoTracking().Include(a => a.Article).Where(a => a.CategoryId == categoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kategori bulunamadı", "categoryId"));
            return new DataResult(ResultStatus.Success, category);

        }

        public async Task<IDataResult> GetCategoryByArticleId(int articleId)
        {
            IQueryable<CategoryAndArticle> query = DbContext.Set<CategoryAndArticle>();
            var article = query.AsNoTracking().Include(a => a.Category).Where(a => a.ArticleId == articleId);
            if (article is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "articleId"));
            return new DataResult(ResultStatus.Success, article);
        }

        public async Task<IDataResult> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.CategoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kategori bulunmamakta", "categoryId"));
            var article = await DbContext.Articles.SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.ArticleId);
            if (article is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunmamakta", "articleId"));

            var categoryAndArticle = Mapper.Map<CategoryAndArticle>(categoryAndArticlesUpdateDto);

            DbContext.CategoryAndArticles.Remove(categoryAndArticle);
            return new DataResult(ResultStatus.Success, categoryAndArticle);
        }

        public async Task<IDataResult> UpdateAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto)
        {
            var article = await DbContext.Articles.Include(ab => ab.ArticlePictures).SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.ArticleId);
            if (article is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunamadı", "ArticleId"));
            var category = await DbContext.Categories.Include(ab => ab.CategoryAndArticles).SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.CategoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kategori bulunamadı", "CategoryId"));

            var categoryAndArticle = Mapper.Map<CategoryAndArticle>(categoryAndArticlesUpdateDto);
            categoryAndArticle.Article = article;
            categoryAndArticle.Category = category;
            article.CategoryAndArticles.Add(categoryAndArticle);
            article.ModifiedDate = DateTime.Now;
            category.CategoryAndArticles.Add(categoryAndArticle);
            category.ModifiedDate = DateTime.Now;
            DbContext.CategoryAndArticles.Update(categoryAndArticle);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, categoryAndArticle);
        }
    }
}

