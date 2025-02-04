using System.Security.Claims;
using BookLibrary.Data;
using BookLibrary.Entities;
using BookLibrary.Models;
using BookLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, AppDbContext context) : Controller
    {
        
        //private UserManager<User> _userManager;

        //public AuthController(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            var id = this.User.FindFirst(ClaimTypes.Name);
            
            //id.Subject;
            //return Ok(this.User);
            //IQueryable<bool> user = context.Users.Select(s => s.Id.ToString() == id.);
            //user.w
            var user = context.Users.FirstOrDefault(u => u.Username == id!.Value);


            return Ok($"You are authenticated!{user?.Username} {user?.Id}");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        }
    }
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
}
