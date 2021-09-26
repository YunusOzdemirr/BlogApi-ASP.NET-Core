using System;
using AutoMapper;
using CmnSoftwareBackend.Data.Concrete.EntityFramework.Contexts;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class ManagerBase
    {
        protected CmnDbContext DbContext { get; }

        protected IMapper Mapper { get; }

        public ManagerBase(CmnDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

    }
}
