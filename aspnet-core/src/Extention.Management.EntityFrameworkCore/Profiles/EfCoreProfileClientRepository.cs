﻿using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.Profiles
{
    public class EfCoreProfileClientRepository : EfCoreRepository<ManagementDbContext, ProfileClient, Guid>, IProfileClientRepository
    {
        public EfCoreProfileClientRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}
