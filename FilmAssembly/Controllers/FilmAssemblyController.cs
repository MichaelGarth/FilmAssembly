using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAssembly.Data;
using FilmAssembly.Logic.Handlers;
using FilmAssembly.Logic.Models;
using FilmAssembly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FilmAssembly.Controllers
{
    public class FilmAssemblyController : Controller
    {
        private readonly FilmAssemblyContext _context;
        private readonly IImageHandler _imageHandler;

        public FilmAssemblyController(
            FilmAssemblyContext context,
            IImageHandler imageHandler)
        {
            _context = context;
            _imageHandler = imageHandler;
        }

        public async Task<IActionResult> FilmAssembly()
        {
            return View(await _context.Media.ToListAsync());
        }

        public async Task<IActionResult> Details(int? mediaId)
        {
            if (mediaId == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .FirstOrDefaultAsync(m => m.MediaId == mediaId);

            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MediaId,Poster,Name,MediaType,Genres,Rating,Platform")] Media media)
        {
            if (ModelState.IsValid)
            {
                _context.Add(media);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(FilmAssembly));
            }

            return View(media);
        }
        
        public async Task<IActionResult> Edit(int? mediaId)
        {
            if (mediaId == null)
            {
                return NotFound();
            }

            var media = await _context.Media.FindAsync(mediaId);
            if (media == null)
            {
                return NotFound();
            }

            var mediaEdit = new MediaEdit
            {
                MediaId = media.MediaId,
                Genres = media.Genres,
                MediaType = media.MediaType,
                Name = media.Name,
                Platform = media.Platform,
                Poster = media.Poster,
                PosterLinks = await GetRefreshedPosters(media.Name, media.Poster),
                Rating = media.Rating
            };

            return View(mediaEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int mediaId, [Bind("MediaId,Poster,Name,MediaType,Genres,Rating,Platform")] MediaEdit media)
        {
            if (mediaId != media.MediaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedMedia = new Media
                    {
                        MediaId = media.MediaId,
                        Genres = media.Genres,
                        MediaType = media.MediaType,
                        Name = media.Name,
                        Platform = media.Platform,
                        Poster = media.Poster,
                        Rating = media.Rating
                    };

                    _context.Update(updatedMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.MediaId))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(FilmAssembly));
            }

            return View(media);
        }
        
        public async Task<IActionResult> Delete(int? mediaId)
        {
            if (mediaId == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .FirstOrDefaultAsync(m => m.MediaId == mediaId);

            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int mediaId)
        {
            var media = await _context.Media.FindAsync(mediaId);
            _context.Media.Remove(media);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(FilmAssembly));
        }

        private bool MediaExists(int mediaId)
        {
            return _context.Media.Any(e => e.MediaId == mediaId);
        }

        private async Task<List<string>> GetPosters(string mediaName)
        {
            var posters =  await _imageHandler.GetImagePoster(mediaName);

            return posters.Items.Select(x => x.Link).ToList();
        }

        private async Task<List<string>> GetRefreshedPosters(string mediaName, string currentItem)
        {
            var posters = await _imageHandler.GetRefreshedPosters(mediaName, currentItem);

            return posters.Items.Select(x => x.Link).ToList();
        }
    }
}
