using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Files
{
    public class FileDto
    {
        public IFormFile File { get; set; }
    }
}
