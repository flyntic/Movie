

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Movie.Models;
using Movie.Options;
using Newtonsoft.Json;

namespace Movie.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IMemoryCache memoryCache;

        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        private HttpClient httpClient { get; set; }

        public MovieApiService(IHttpClientFactory httpClientFactory, IOptions<MovieApiOptions> options, 
            IMemoryCache memoryCache)
        {
            //BaseUrl = "https://omdbapi.com/";
            //ApiKey = "5b9b7798";

            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;

            httpClient = httpClientFactory.CreateClient();
            this.memoryCache = memoryCache;
        }

        public async Task<MovieApiResponse> SearchByTitleAsync(string title)
        {
            MovieApiResponse result;

            if (!memoryCache.TryGetValue(title,out result))
            {
                Console.WriteLine("REQUEST");
                var response = await httpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MovieApiResponse>(json);

                if (result.Response == "False")
                    throw new Exception(result.Error);

                memoryCache.Set(title, result);
            }
            else
            {
                Console.WriteLine("Read Cache");
            }

            return result;
        }

        public async Task<Models.Cinema> SearchByIdAsync(string id)
        {
            Cinema result;

            if (!memoryCache.TryGetValue(id, out result))
            {
                Console.WriteLine("Cache save");
                var response = await httpClient.GetAsync($"{BaseUrl}?&apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Cinema>(json);

                if (result.Response == "False")
                    throw new Exception(result.Error);

                memoryCache.Set(id, result);
            }
            else
            {
                Console.WriteLine("Cache read");
            }



            return result;
        }
    }
}
