using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanner.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    public class InvitationController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}