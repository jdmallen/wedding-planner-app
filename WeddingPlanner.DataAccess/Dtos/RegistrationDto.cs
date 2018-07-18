using System.ComponentModel.DataAnnotations;
using JDMallen.Toolbox.Dtos;

namespace WeddingPlanner.Models.Dtos
{
	public class RegistrationDto : RegisterDto
	{
		[Required]
		public string InvitationCode { get; set; }
	}
}
