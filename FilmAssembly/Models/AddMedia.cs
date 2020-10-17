using FilmAssembly.Logic.Models;

namespace FilmAssembly.Models
{
    public class AddMedia
    {
        public ImagePosters PosterLinks { get; set; }

        public string Media { get; set; }

        public string MediaType { get; set; }
    }
}
