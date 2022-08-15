using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.AttachmentsStore
{
    public class EfCoreAttachmentsStoreRepository : EfCoreRepository<ManagementDbContext, AttachmentsStore, Guid>, IAttachmentsStoreRepository
    {
        public EfCoreAttachmentsStoreRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}
