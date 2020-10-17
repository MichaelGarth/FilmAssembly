using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmAssembly.Models;
using FilmAssembly.Logic;

namespace FilmAssembly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageHandler _imageHandler;

        public HomeController(ILogger<HomeController> logger, IImageHandler imageHandler)
        {
            _logger = logger;
            _imageHandler = imageHandler;

        }

        [HttpGet]
        public IActionResult Start(bool isInvalidInput)
        {
            if (isInvalidInput)
            {
                return View(new Start
                {
                    Error = "Not all inputs are filled out correctly, please try again..."
                });
            }

            return View(new Start{ Error = "" });
        }

        public async Task<IActionResult> AddMedia(string media, string mediaType)
        {
            if (media == null || mediaType == null)
            {
                return RedirectToAction("Start", new { isInvalidInput = true });
            }

            var imagePosters = await _imageHandler.GetImagePoster(media + " poster");

            var addMediaModel = new AddMedia
            {
                PosterLinks = imagePosters,
                MediaType = mediaType,
                Media = media
            };

            return View(addMediaModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
