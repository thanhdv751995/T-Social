using Extention.Management.ProfileGroupTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Profiles
{
    public class UpdateMutilScriptProfileDto
    {
        public List<UpdateProfileDto> UpdateProfileDto { get; set; }
        public List<string> ScriptDefaultName { get; set; }
        public DeleteGroupTypeWithProfileDto DeleteGroupTypeWithProfileDto { get; set; }
        public AddProfileWithGroupTypeDto AddProfileWithGroupTypeDto { get; set; }
    }
}
