using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JDMallen.Toolbox.Constants;
using JDMallen.Toolbox.Dtos;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Factories;
using JDMallen.Toolbox.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.DataAccess.Dtos;
using WeddingPlanner.DataAccess.Entities.Identity;

namespace WeddingPlanner.Web.Controllers
{
	[Route("api/account")]
	public class ApiAccountController : Controller
	{
		private readonly IJwtTokenFactory _jwtTokenFactory;
		private readonly Settings _settings;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public ApiAccountController(
			IJwtTokenFactory jwtTokenFactory,
			Settings settings, 
			SignInManager<AppUser> signInManager, 
			UserManager<AppUser> userManager)
		{
			_jwtTokenFactory = jwtTokenFactory;
			_settings = settings;
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
		public async Task<IActionResult> CheckPassword([FromBody] LoginDto request)
		{
			var result = await _userManager.PasswordValidators
											.First(x => x.GetType() == typeof(CustomPasswordValidator<AppUser>))
											.ValidateAsync(_userManager,
															new AppUser
															{
																UserName = request.Email,
																Email = request.Email
															},
															request.Password);

			return Ok(ModelState.AddIdentityErrors(result));
		}
		
		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginEmail([FromBody] LoginDto login)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			var user = await GetUserIdentity(login);
			if (user == null)
			{
				ModelState.AddIdentityError("BadLogin", "Invalid email or password.");
				return BadRequest(ModelState);
			}

//			var claims = await _userManager.GetClaimsAsync(user);

			var token = _jwtTokenFactory.GenerateToken(user);

			var returnData = new
			{
				token,
//				user
			};
			return Ok(returnData);
		}

		private async Task<ClaimsIdentity> GetUserIdentity(LoginDto login)
		{
			if (login.Email.IsNullOrWhiteSpace() || login.Password.IsNullOrWhiteSpace())
				return null;

			var user = await _userManager.FindByEmailAsync(login.Email);
			if (user == null) return null;
			
			if (await _userManager.CheckPasswordAsync(user, login.Password))
			{
				return _jwtTokenFactory.GenerateClaimsIdentity(login.Email, user.Id);
			}

			return null;
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
							OAuthSchemes.GitHub);
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("login/google")]
		public IActionResult LoginGoogle(string returnUrl = "/")
		{
			return Challenge(new AuthenticationProperties
							{
								RedirectUri = returnUrl
							},
							OAuthSchemes.Google);
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationDto register)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var appUser = new AppUser
			{
				Email = register.Email,
				DisplayName = register.DisplayName,
				InvitationCode = register.InvitationCode,
				UserName = register.Email,
			};
			var result = await _userManager.CreateAsync(appUser, register.Password);

			if (!result.Succeeded) return BadRequest(ModelState.AddIdentityErrors(result));

			return CreatedAtAction("LoginEmail",
									new LoginDto()
									{
										Email = register.Email
									});
		}
	}
}
