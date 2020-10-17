using System.Net.Http;
using System.Threading.Tasks;
using FilmAssembly.Logic.Models;

namespace FilmAssembly.Logic
{
    public class ImageHandler : IImageHandler
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string CustomSearchApiUrl = "https://www.googleapis.com/customsearch/v1";
        private const string ApiKey = "AIzaSyDLAESRyLjbSOztJriDaYpNhpFFLx2hz3o";
        private const string SearchEngineKey = "c0ca00e7dfd9453a7";

        public async Task<ImagePosters> GetImagePoster(string image)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"{CustomSearchApiUrl}?q={image}&num=3&start=1&searchType=image&key={ApiKey}&cx={SearchEngineKey}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ImagePosters>();
            }

            return new ImagePosters();
        }
    }
}
