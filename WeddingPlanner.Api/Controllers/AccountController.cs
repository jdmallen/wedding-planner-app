using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
			return Ok("Hello, world.");
		}

		[HttpPost]
		[Route("check")]
		public IActionResult CheckPassword([FromBody] LoginDto request)
		{
			return Ok(_passwordChecker.CheckPassword(request.Password));
		}

		// POST api/values
		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public IActionResult Login([FromBody] LoginDto request)
		{
			if (request.Email != "user" || request.Password != "password")
				return BadRequest("Could not verify username and password");

			var claims = new[]
			{
				new Claim(ClaimTypes.Name, request.Email)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
#if DEBUG
				issuer: "localhost",
				audience: "localhost",
#else
				issuer: "jdmallen.com",
			    audience: "jdmallen.com",
#endif
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token)
			});
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
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_settings.JwtExpireDays));

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
