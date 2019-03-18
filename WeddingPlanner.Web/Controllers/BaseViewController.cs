using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanner.Web.Controllers
{
	[Route("")]
	public class BaseViewController : Controller
	{
		private readonly IHostingEnvironment _hostingEnvironment;

		public BaseViewController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{
			return View(
				_hostingEnvironment.EnvironmentName.IndexOf(
					"development",
					StringComparison.CurrentCultureIgnoreCase)
				!= -1);
		}
	}
}
