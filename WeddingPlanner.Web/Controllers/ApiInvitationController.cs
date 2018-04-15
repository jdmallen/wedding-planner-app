using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanner.Web.Controllers
{
	[Authorize]
	[Route("api/invitation")]
    public class ApiInvitationController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}