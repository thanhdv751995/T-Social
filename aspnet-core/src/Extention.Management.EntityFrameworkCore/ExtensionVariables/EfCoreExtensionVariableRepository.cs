﻿using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ExtensionVariables
{
    public class EfCoreExtensionVariableRepository : EfCoreRepository<ManagementDbContext, ExtensionVariable, Guid>, IExtensionVariableRepository
    {
        public EfCoreExtensionVariableRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}
