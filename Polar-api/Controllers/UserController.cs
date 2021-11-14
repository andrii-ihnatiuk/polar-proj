using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;	
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Polar.Models;

namespace Polar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly PolarContext _context;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<ApplicationSettings> appSettings, PolarContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<Object> Register(UserRegisterModel model)
        {
            var newUser = new User() {
                UserName = model.UserName,
                Email = model.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, model.Password);
                return Ok(result); 
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model) 
        {
            var user = this.IsEmailLogin(model.Login) 
                ? await _userManager.FindByEmailAsync(model.Login) // Если логин по email то и пользователя ищем по email
                : await _userManager.FindByNameAsync(model.Login); 
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var key = Encoding.UTF8.GetBytes(_appSettings.AuthOptions.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            } else 
                return BadRequest(new { message = "Username or password is incorrect" });
        }

        private bool IsEmailLogin(string login) 
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            var result = Regex.IsMatch(login, pattern) ? true : false;
            return result;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            // var identity = (ClaimsIdentity)User.Identity;
            // IEnumerable<Claim> claims = identity.Claims;
            // var userId = claims.First(c => c.Type == "UserID").Value;
            
            // Do the same as upper one
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var userInfo = new { username = user.UserName, email = user.Email }; // User info to be sent to client
            
            // Getting all locations from DB
            var query = from location in _context.Locations select new { location.Id, location.Name, location.NumberOfMarkers };
            var locations = query.ToList();

            var areas = new List<object>(); // List of areas and found markers
            foreach (var location in locations)
            {
                var res = from marker in _context.Markers
                        where (marker.LocationId == location.Id && marker.Users.Any(u => u.Id == userId))
                        select new { marker.Id, marker.Type };
                areas.Add(new { 
                    name = location.Name,
                    markers = res.ToList(),
                    totalMarkers = location.NumberOfMarkers
                });
            }
         
            return Ok(new { info = userInfo, data = areas });
        }

    }
}