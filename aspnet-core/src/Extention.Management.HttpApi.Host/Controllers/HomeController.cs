using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Extention.Management.Controllers
{
    public class HomeController : AbpController
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<ActionResult> Index()
        {
            var filePath = Path.Combine(_env.WebRootPath,"", "index.html");
            if (!System.IO.File.Exists(filePath))
            {
                return View();
            }

            using (var reader = new StreamReader(filePath))
            {
                var fileContent = await reader.ReadToEndAsync();

                return Content(fileContent, "text/html", Encoding.UTF8);
            }
        }
    }
}
