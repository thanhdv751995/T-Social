﻿using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.StatussStore
{
    public class EfCoreStatusStoreRepository : EfCoreRepository<ManagementDbContext, StatusStore, Guid>, IStatusStoreRepository
    {
        public EfCoreStatusStoreRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}