using System.Collections.Generic;

namespace FilmAssembly.Models
{
    public class MediaEdit
    {
        public int MediaId { get; set; }

        public string Poster { get; set; }

        public List<string> PosterLinks { get; set; }

        public string Name { get; set; }

        public string MediaType { get; set; }

        public string Genres { get; set; }

        public int Rating { get; set; }

        public string Platform { get; set; }
    }
}
