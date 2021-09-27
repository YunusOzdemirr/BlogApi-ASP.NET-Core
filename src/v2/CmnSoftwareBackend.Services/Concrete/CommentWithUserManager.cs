using System;
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

        public Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> GetByIdAsync(int commentWithUserId, bool includeArticle)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int commentWithUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> UpdateAsync(CommentWithUserUpdateDto commentWithUserUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

