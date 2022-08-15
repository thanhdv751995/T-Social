﻿using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.Commenteds
{
    public class EfCoreCommentedRepository : EfCoreRepository<ManagementDbContext, Commented, Guid>,
            ICommentedRepository
    {
        public EfCoreCommentedRepository(
        IDbContextProvider<ManagementDbContext> dbContextProvider)
       : base(dbContextProvider)
        {
        }
    }
}
