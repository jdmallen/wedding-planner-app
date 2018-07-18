using JDMallen.Toolbox.Models;

namespace WeddingPlanner.Models.Parameters
{
	public class AppUserQueryParameters : QueryParameters
	{
		public string Email { get; set; }

		public string Username { get; set; }
	}
}