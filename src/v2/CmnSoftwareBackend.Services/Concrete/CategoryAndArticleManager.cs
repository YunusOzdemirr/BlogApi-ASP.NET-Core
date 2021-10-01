using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
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
    public class CategoryAndArticleManager:ManagerBase,ICategoryAndArticleService
    {

        public CategoryAndArticleManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IDataResult> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto)
        {
            var category =await DbContext.Categories.AsNoTracking().Include(ab=>ab.CategoryAndArticles).SingleOrDefaultAsync(a=>a.Id== categoryAndArticleAddDto.CategoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir kategori bulunmamakta","categoryId"));

            var article = await DbContext.Articles.AsNoTracking().Include(ab=>ab.CategoryAndArticles).SingleOrDefaultAsync(a=>a.Id==categoryAndArticleAddDto.ArticleId);
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
            return new DataResult(ResultStatus.Success,categoryAndArticle);

        }

        public async Task<IDataResult> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto)
        {
            var category =await DbContext.Categories.SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.CategoryId);
            if (category is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(),new Error("Böyle bir kategori bulunmamakta","categoryId"));
            var article = await DbContext.Articles.SingleOrDefaultAsync(a => a.Id == categoryAndArticlesUpdateDto.ArticleId);
            if (article is null)
                throw new NotFoundArgumentException(Messages.General.ValidationError(), new Error("Böyle bir makale bulunmamakta", "articleId"));

            var categoryAndArticle = Mapper.Map<CategoryAndArticle>(categoryAndArticlesUpdateDto);

            DbContext.CategoryAndArticles.Remove(categoryAndArticle);
            return new DataResult(ResultStatus.Success, categoryAndArticle);
        }
    }
}

