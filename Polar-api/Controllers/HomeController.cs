using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Polar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private IConfiguration _config;
        public HomeController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            var fileName = "polar.apk";
            var filePath = _config.GetValue<string>("Paths:FilesPath") + fileName;
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
          
            return File(bytes, "application/vnd.android.package-archive", fileName);
        }
    }
}