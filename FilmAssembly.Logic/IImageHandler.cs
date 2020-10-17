using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilmAssembly.Logic.Models;

namespace FilmAssembly.Logic
{
    public interface IImageHandler
    {
        Task<ImagePosters> GetImagePoster(string image);
    }
}
