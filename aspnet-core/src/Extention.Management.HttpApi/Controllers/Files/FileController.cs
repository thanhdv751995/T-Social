using Extention.Management.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Files
{
    [Microsoft.AspNetCore.Mvc.Route("api/files")]
    public class FileController : ManagementController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<IEnumerable<UrlFileDto>> UploadAsync([FromForm] FileDto dto)
        {
            return await _fileService.UploadFileAsync(new List<IFormFile> { dto.File });
        }
    }
}
