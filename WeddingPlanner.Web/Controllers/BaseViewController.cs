using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanner.Web.Controllers
{
	[Route("")]
	public class BaseViewController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
