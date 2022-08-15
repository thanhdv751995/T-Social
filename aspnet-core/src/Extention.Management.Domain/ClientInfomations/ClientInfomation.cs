using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfomation : AuditedAggregateRoot<Guid>
    {
        public Guid IdUser { get; set; }
        public string ClientId { get; set; }
        public string NameUser { get; set; }
        public string DayOfBirth { get; set; }
        private ClientInfomation()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ClientInfomation(
               Guid id,
               [NotNull] Guid idUser,
               [NotNull] string clientId,
               [NotNull] string nameUser,
               string dayOfBirth
           )
           : base(id)
        {
            IdUser = idUser;
            ClientId = clientId;
            NameUser = nameUser;
            DayOfBirth = dayOfBirth;
        }
    }
}
