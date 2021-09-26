using System;
using System.Threading.Tasks;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class CommentWithoutUserManager : ManagerBase, ICommentWithoutUserService
    {
        public CommentWithoutUserManager(CmnDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Task<IDataResult> AddAsync(CommentWithoutUserAddDto commentWithoutUserAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> DeleteAsync(int commentWithoutUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> GetByIdAsync(int commentWithoutUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int commentWithoutUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> UpdateAsync(CommentWithoutUserUpdateDto commentWithoutUserUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

