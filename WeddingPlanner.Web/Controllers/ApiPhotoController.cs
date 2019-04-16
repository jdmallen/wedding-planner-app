using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.Services.Interfaces;

namespace WeddingPlanner.Web.Controllers
{
	[Route("api/photo")]
	public class ApiPhotoController : Controller
	{
		private readonly Settings _settings;
		private readonly IPhotoService _photoService;

		public ApiPhotoController(Settings settings, IPhotoService photoService)
		{
			_settings = settings;
			_photoService = photoService;
		}

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetPhotos()
		{
			var photos = _photoService.ListAllPhotos();
			return Ok(await photos.ToList());
		}
	}
}
