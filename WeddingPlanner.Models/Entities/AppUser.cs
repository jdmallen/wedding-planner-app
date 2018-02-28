using JDMallen.Toolbox.Models;

namespace WeddingPlanner.Models.Entities
{
	public class AppUser : IdUser
	{
		public string DisplayName { get; set; }

		public string InvitationCode { get; set; }
	}
}
