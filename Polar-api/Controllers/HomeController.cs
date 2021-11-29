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
                        select new RatingUserModel { Username = users.UserName, Score = users.Score }).Take(4).ToListAsync();
            return Ok(new RatingModel { Succeeded = true, Rating = res });
        }

        [HttpGet("markers")]
        public async Task<IActionResult> Markers()
        {
            // Getting all locations from DB
            var query = from location in _context.Locations select new { location.Id, location.Name, location.NumberOfMarkers };
            var locations = query.ToList();

            var areas = new List<object>(); // List of areas and markers
            foreach (var location in locations)
            {
                var res = await (
                            from marker in _context.Markers
                            where (marker.LocationId == location.Id)
                            select new { marker.Id, marker.QrCode,  marker.Type, storyId = marker.Story.Id, storyText = marker.Story.Text }).ToListAsync();
                areas.Add(new { 
                    name = location.Name,
                    markers = res.ToList(),
                });
            }

            return Ok(new { succeeded = true, data = areas });
        }
    }
}