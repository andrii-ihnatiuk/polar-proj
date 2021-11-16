using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Polar.Models;


namespace Polar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationSettings _appSettings;
        private readonly PolarContext _context;
        public HomeController(IOptions<ApplicationSettings> appSettings, PolarContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
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

        [HttpGet("rating")]
        public async Task<IActionResult> Rating()
        {
            var res = await (
                        from users in _context.Users
                        orderby users.Score descending
                        select new { username = users.UserName, score = users.Score }).Take(4).ToListAsync();
            return Ok(new { succeeded = true, rating = res });
        }
    }
}