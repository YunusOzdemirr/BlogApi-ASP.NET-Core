using System;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts
{
    public class CmnDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Article >Articles { get; set; }
        public DbSet<CommentWithoutUser> CommentWithoutUsers{ get; set; }
        public DbSet<CommentWithUser> CommentWithUsers{ get; set; }
        public DbSet<ArticlePicture> ArticlePictures{ get; set; }
        public DbSet<CategoryAndArticle> CategoryAndArticles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new OperationClaimMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserOperationClaimMap());
            modelBuilder.ApplyConfiguration(new UserNotificationMap());
            modelBuilder.ApplyConfiguration(new LogMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.ApplyConfiguration(new UserPictureMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CommentWithUserMap());
            modelBuilder.ApplyConfiguration(new CommentWithoutUserMap());
            modelBuilder.ApplyConfiguration(new CategoryAndArticleMap());
            modelBuilder.ApplyConfiguration(new ArticlePictureMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=localhost;Database=Cmn;User=sa;Password=Password123@jkl#;");
        }
    }
}
