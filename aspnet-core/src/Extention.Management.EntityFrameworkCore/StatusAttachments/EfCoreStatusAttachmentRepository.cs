using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.StatusAttachments
{
    public class EfCoreStatusAttachmentRepository : EfCoreRepository<ManagementDbContext, StatusAttachment, Guid>, IStatusAttachmentRepository
    {
        public EfCoreStatusAttachmentRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}
