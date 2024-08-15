using Microsoft.Extensions.Configuration;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;
using Newtonsoft.Json;

namespace NewsApp.Core.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        
        #region Ctor

        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets top story ids
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetTopStoryIds()
        {
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_configuration["TopStoriesUrl"])
                };

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<int>>(responseStream);
                    return result;
                }

                return Enumerable.Empty<int>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets single sotry item by identifier
        /// </summary>
        /// <param name="sotryId"></param>
        /// <returns></returns>
        public async Task<NewsItemModel?> GetStoryItem(int sotryId)
        {
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(string.Format(_configuration["GetStoryItemUrl"], sotryId))
                };

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NewsItemModel?>(responseStream);
                    return result;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
