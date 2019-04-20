using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;
using WeddingPlanner.Services.Interfaces;

namespace WeddingPlanner.Api.Controllers
{
	// [Authorize]
	[Route("api/invitation")]
	public class ApiInvitationController : Controller
	{
		// todo go back to interface
		private readonly IInvitationService _invitationService;

		public ApiInvitationController(IInvitationService invitationService)
		{
			_invitationService = invitationService;
		}

		[HttpPost]
		[Route("upsert")]
		public async Task<IActionResult> Create(Invitation invitation)
		{
			return Ok(await _invitationService.Upsert(invitation));
		}

		[HttpGet]
		[Route("find")]
		public async Task<IActionResult> Find(InvitationQueryParameters query)
		{
			return Ok(await _invitationService.FindPaged(query));
		}

		[HttpGet]
		[Route("types")]
		public IActionResult GetInvitationTypes()
		{
			return Ok(_invitationService.GetAllInvitationTypes());
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> ListInvitations()
		{
			return Ok(await _invitationService.ListAllInvitations().ToList());
		}
	}
}
