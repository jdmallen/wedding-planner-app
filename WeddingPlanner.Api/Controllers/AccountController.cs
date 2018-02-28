using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JDMallen.Toolbox.Dtos;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Factories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.Models.Dtos;
using WeddingPlanner.Models.Entities;

namespace WeddingPlanner.Api.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IJwtTokenFactory _jwtTokenFactory;
		private readonly Settings _settings;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly WpIdentityContext _identityContext;

		public AccountController(
			IJwtTokenFactory jwtTokenFactory,
			Settings settings, 
			SignInManager<AppUser> signInManager, 
			UserManager<AppUser> userManager,
			WpIdentityContext identityContext)
		{
			_jwtTokenFactory = jwtTokenFactory;
			_settings = settings;
			_signInManager = signInManager;
			_userManager = userManager;
			_identityContext = identityContext;
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
			return Ok(/*_passwordChecker.CheckPassword(request.Password)*/);
		}
		
		[AllowAnonymous]
		[HttpPost]
		[Route("login/email")]
		public async Task<IActionResult> LoginEmail([FromBody] LoginDto login)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result =
				await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

			if (!result.Succeeded) return Forbid();

			var appUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
			var token = GenerateJwtToken(login.Email, appUser);

			return Ok(new
			{
				token
			});
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("login/github")]
		public IActionResult LoginGithub(string returnUrl = "/")
		{
			return Challenge(new AuthenticationProperties
							{
								RedirectUri = returnUrl
							},
							"GitHub");
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationDto register)
		{
			var appUser = new AppUser
			{
				Email = register.Email,
				DisplayName = register.DisplayName,
				InvitationCode = register.InvitationCode,
				UserName = register.Email,
			};
			var result = await _userManager.CreateAsync(appUser, register.Password);

			if (!result.Succeeded) return BadRequest(ModelState.AddIdentityErrors(result));

			await _identityContext.Users.AddAsync(appUser);
			await _identityContext.SaveChangesAsync();
			await _signInManager.SignInAsync(appUser, false);
			
			var token = GenerateJwtToken(register.Email, appUser);
			return Ok(new
			{
				token
			});
		}

		private string GenerateJwtToken(string email, AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.ShortId)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey));
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
