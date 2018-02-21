using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanner.Models.Domain;
using WeddingPlanner.Models.Dtos;
using WeddingPlanner.Service.Interfaces;

namespace WeddingPlanner.Api.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly Settings _settings;
		private readonly IPasswordCheckerService _passwordChecker;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public AccountController(
			Settings settings, 
			IPasswordCheckerService passwordChecker, 
			SignInManager<AppUser> signInManager, 
			UserManager<AppUser> userManager)
		{
			_settings = settings;
			_passwordChecker = passwordChecker;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> Get()
		{
			return Ok("Hello.");
		}

		[HttpGet]
		[Authorize]
		[Route("protected")]
		public async Task<IActionResult> Protected()
		{
			return Ok("Success!");
		}

		[HttpPost]
		[Route("check")]
		public IActionResult CheckPassword([FromBody] LoginDto request)
		{
			return Ok(_passwordChecker.CheckPassword(request.Password));
		}
		
		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto login)
		{
			var result =
				await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

			if (!result.Succeeded) return Forbid();

			var appUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
			var token = await GenerateJwtToken(login.Email, appUser);

			return Ok(new
			{
				token
			});
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto register)
		{
			var appUser = new AppUser
			{
				UserName = register.Email,
				Email = register.Email
			};
			var result = await _userManager.CreateAsync(appUser, register.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(appUser, false);
				var token = await GenerateJwtToken(register.Email, appUser);

				return Ok(new
				{
					token
				});
			}

			return StatusCode(500, result.Errors);
		}

		private async Task<string> GenerateJwtToken(string email, AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.ShortId)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.AddMinutes(_settings.JwtExpireMinutes);

			var token = new JwtSecurityToken(
				_settings.JwtIssuer,
				_settings.JwtIssuer,
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
