using Extention.Management.Image;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Image
{
    [Microsoft.AspNetCore.Mvc.Route("api/image")]
    public class ImageController : ManagementController
    {
        private readonly ImageAppService _imageAppService;
        public ImageController(ImageAppService imageAppService)
        {
            _imageAppService = imageAppService;
        }
        [HttpGet("Base64Encode")]
        public string Base64Encode(string plainText)
        {
            return _imageAppService.Base64Encode(plainText);
        }
        [HttpGet("Base64Decode")]
        public string Base64Decode(string base64EncodedData)
        {
            return _imageAppService.Base64Decode(base64EncodedData);
        }
    }
}
