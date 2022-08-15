using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Extention.Management.ViewClientUsingScripts
{
    public class ClientUsingScriptView : Entity
    {
        public string UserName { get; set; }
        public string NameFaceBook { get; set; }
        public string ScriptName { get; set; }
        public Type.Type Type { get; set; }
        public string Value { get; set; }
        public bool IsActiveScript { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActiveClientUsingScript { get; set; }
        public override object[] GetKeys()
        {
            return null;
        }
    }
}
