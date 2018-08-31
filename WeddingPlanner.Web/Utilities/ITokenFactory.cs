using System.Security.Claims;
using System.Threading.Tasks;
using JDMallen.Toolbox.Factories;
using WeddingPlanner.DataAccess.Entities.Identity;

namespace WeddingPlanner.Web.Utilities
{
	public interface ITokenFactory : IJwtTokenFactory
	{
		Task<ClaimsIdentity> GenerateClaimsIdentity(AppUser user);
	}
}
