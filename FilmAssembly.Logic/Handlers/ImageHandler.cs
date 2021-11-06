using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FilmAssembly.Logic.Models;

namespace FilmAssembly.Logic.Handlers
{
    public class ImageHandler : IImageHandler
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string CustomSearchApiUrl = "https://www.googleapis.com/customsearch/v1";
        private const string ApiKey = "AIzaSyDLAESRyLjbSOztJriDaYpNhpFFLx2hz3o";
        private const string SearchEngineKey = "c0ca00e7dfd9453a7";

        public async Task<Posters> GetImagePoster(string image)
        {
            return await FetchFromInternet(image, 1, 4);
        }

        public async Task<Posters> GetRefreshedPosters(string image, string currentImage)
        {
            var posters = await FetchFromInternet(image, 1, 4);

            if (posters.Items == null)
            {
                return new Posters
                {
                    Items = new List<Links>
                    {
                        new Links
                        {
                            Link = "https://cdn.pixabay.com/photo/2017/08/25/21/47/confused-2681507_960_720.jpg"
                        }
                    }
                };
            }

            var duplicate = posters.Items.FirstOrDefault(x => x.Link == currentImage);

            if (duplicate != null)
            {
                posters.Items.Remove(duplicate);

                return posters;
            }

            var lastPoster = posters.Items.LastOrDefault();
            posters.Items.Remove(lastPoster);

            return posters;
        }

        private async Task<Posters> FetchFromInternet(string image, int start, int amountOfImages)
        {
            var response = await _httpClient.GetAsync(
                $"{CustomSearchApiUrl}?q={image} poster&num={amountOfImages}&start={start}&searchType=image&key={ApiKey}&cx={SearchEngineKey}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Posters>();
            }

            return new Posters();
        }
    }
}
