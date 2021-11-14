using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Polar.Models;


namespace Polar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationSettings _appSettings;
        public HomeController(ApplicationSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            var fileName = "polar.apk";
            var filePath = _appSettings.Paths.FilesPath + fileName;
            if (!System.IO.File.Exists(filePath)) 
            {
                return NotFound();
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
          
            return File(bytes, "application/vnd.android.package-archive", fileName);
        }
    }
}