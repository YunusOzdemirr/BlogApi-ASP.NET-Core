using System;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace CmnSoftwareBackend.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CmnDbContext>();
            serviceCollection.AddSingleton<IJwtHelper, JwtHelper>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IOperationClaimService, OperationClaimManager>();
            serviceCollection.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<IArticlePictureService, ArticlePictureManager>();
            serviceCollection.AddScoped<ICommentWithoutUserService, CommentWithoutUserManager>();
            serviceCollection.AddScoped<ICommentWithUserService, CommentWithUserManager>();
            serviceCollection.AddScoped<IUserService, UserManager>();
            serviceCollection.AddScoped<IUserAuthService, UserAuthManager>();
            serviceCollection.AddScoped<IMailService, MailManager>();


            return serviceCollection;
        }
    }
}
