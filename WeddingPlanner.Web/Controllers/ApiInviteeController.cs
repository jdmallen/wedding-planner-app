using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;
using WeddingPlanner.Services.Interfaces;

namespace WeddingPlanner.Web.Controllers
{
	// [Authorize]
	[Route("api/invitee")]
    public class ApiInviteeController : Controller
	{
		private readonly IInviteeService _inviteeService;

		public ApiInviteeController(IInviteeService inviteeService)
		{
			_inviteeService = inviteeService;
		}

		[HttpPost]
		[Route("upsert")]
		public async Task<IActionResult> Create(Invitee invitee)
		{
			return Ok(await _inviteeService.Upsert(invitee));
		}

		[HttpGet]
		[Route("find")]
		public async Task<IActionResult> Find(InviteeQueryParameters query)
		{
			return Ok(await _inviteeService.FindPaged(query));
		}
		
		[HttpGet]
		[Route("relationships")]
		public async Task<IActionResult> GetRelationships()
		{
			return Ok(await _inviteeService.GetAllRelationshipTypes());
		}

		[HttpGet]
		[Route("mealchoices")]
		public async Task<IActionResult> ListMealChoices()
		{
			return Ok(await _inviteeService.ListMealChoices().ToList());
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> ListInvitees()
		{
			return Ok(await _inviteeService.ListAllInvitees().ToList());
		}

        public IActionResult Index()
        {
            return Ok();
        }
    }
}