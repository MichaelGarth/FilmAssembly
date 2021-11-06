using Microsoft.EntityFrameworkCore;
using FilmAssembly.Models;

namespace FilmAssembly.Data
{
    public class FilmAssemblyContext : DbContext
    {
        public FilmAssemblyContext (DbContextOptions<FilmAssemblyContext> options)
            : base(options)
        {
        }

        public DbSet<Media> Media { get; set; }
    }
}
