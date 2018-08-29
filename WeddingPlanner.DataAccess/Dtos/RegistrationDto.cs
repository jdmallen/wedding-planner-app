using System.ComponentModel.DataAnnotations;
using JDMallen.Toolbox.Dtos;

namespace WeddingPlanner.DataAccess.Dtos
{
	public class RegistrationDto : RegisterDto
	{
		[Required]
		public string InvitationCode { get; set; }
	}
}
