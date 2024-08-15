using Microsoft.Extensions.Configuration;
using NewsApp.Core.Enums;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;
using Newtonsoft.Json;

namespace NewsApp.Core.Services
{
    /// <summary>
    /// News Service class
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly ICacheService _cacheService;
        private readonly IApiService _apiService;

        #region Ctor

        public NewsService(ICacheService cacheService, IApiService apiService)
        {
            _cacheService = cacheService;
            _apiService = apiService;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets all new stories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NewsItemModel>> GetAllStories()
        {
            try
            {
                List<NewsItemModel> storyItemsList = new();
                IEnumerable<NewsItemModel> filteredStoryItems;

                // Check cache whether stories eixst
                var cacheRecords = await _cacheService.RetrieveFromCacheAsync("TopStoriesDataKey");
                if (cacheRecords.CacheStatus == CacheStatusOption.DoesNotExists)
                {
                    // Get story ids from API
                    var storyIds = (await _apiService.GetTopStoryIds()).Take(240);
                    if (storyIds is not null && storyIds.Any())
                    {
                        var tasks = storyIds.Select(async storyId =>
                        {
                            // Get story item, if not null - add to list
                            var storyItem = await _apiService.GetStoryItem(storyId);
                            if (storyItem is not null)
                                storyItemsList.Add(storyItem);
                        });
                        await Task.WhenAll(tasks);

                        // Filter, sort & save items in cache
                        filteredStoryItems = storyItemsList
                                .Where(x => !string.IsNullOrEmpty(x.Url))
                                .Take(200).OrderByDescending(x => x.Id);
                        await _cacheService.SaveToCacheAsync("TopStoriesDataKey", filteredStoryItems);

                        return filteredStoryItems;
                    }
                    else return Enumerable.Empty<NewsItemModel>();
                }

                filteredStoryItems = (IEnumerable<NewsItemModel>)cacheRecords.CacheValue;
                return filteredStoryItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
