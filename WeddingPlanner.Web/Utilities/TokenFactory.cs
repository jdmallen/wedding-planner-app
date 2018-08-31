using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using JDMallen.Toolbox.Constants;
using JDMallen.Toolbox.Factories;
using JDMallen.Toolbox.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WeddingPlanner.DataAccess.Entities.Identity;

namespace WeddingPlanner.Web.Utilities
{
	public class TokenFactory : JwtTokenFactory, ITokenFactory
	{
		private readonly UserManager<AppUser> _userManager;

		public TokenFactory(
			IOptions<JwtOptions> jwtOptions,
			UserManager<AppUser> userManager) : base(jwtOptions)
		{
			_userManager = userManager;
		}

		public async Task<ClaimsIdentity> GenerateClaimsIdentity(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtClaimTypes.UserId, user.NormalizedUserName),
				new Claim(ClaimTypes.Name, user.NormalizedEmail),
				new Claim(ClaimTypes.Email, user.NormalizedEmail),
			};

			var userRoles = await _userManager.GetClaimsAsync(user);

			claims.AddRange(userRoles);

			var identity = new ClaimsIdentity(
				new GenericIdentity(user.NormalizedEmail, "token"),
				claims
			);

			return identity;
		}
	}
}
