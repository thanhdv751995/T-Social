using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Files
{
    public interface IFileDto
    {
        public string Content { get; set; }
        public string Extention { get; set; }
        public long Size { get; set; }
        public string Type { get; }
    }
}
