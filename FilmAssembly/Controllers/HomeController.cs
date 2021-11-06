using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmAssembly.Models;
using FilmAssembly.Logic;
using FilmAssembly.Logic.Handlers;

namespace FilmAssembly.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageHandler _imageHandler;

        public HomeController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }

        [HttpGet]
        public IActionResult FilmAssembly()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
