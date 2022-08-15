using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Files
{
    public interface IFileService
    {
        Base64FileDto ConvertToBase64(IFormFile file);
        IEnumerable<Base64FileDto> ConvertToBase64(IList<IFormFile> files);
        Task<IEnumerable<UrlFileDto>> UploadFileAsync(IList<IFormFile> files);
        Task<UrlFileDto> UploadFileAsync(IFormFile file);
    }
}
