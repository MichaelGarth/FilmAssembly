using System.Threading.Tasks;
using FilmAssembly.Logic.Models;

namespace FilmAssembly.Logic.Handlers
{
    public interface IImageHandler
    {
        Task<Posters> GetImagePoster(string image);
    }
}
