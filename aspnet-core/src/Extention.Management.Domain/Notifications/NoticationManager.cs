using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Notifications
{
    public class NoticationManager : DomainService
    {
        public NoticationManager()
        {

        }

        public Notification CreateAsync(
             string idUser,
            [NotNull] string content,
             string urlAvater,
             string time,
             string href
            )
        {
            return new Notification(
                GuidGenerator.Create(),
                idUser,
                content,
                urlAvater,
                time,
                href
            );
        }
    }
}
