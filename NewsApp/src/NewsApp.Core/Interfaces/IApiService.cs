using NewsApp.Core.Models;

namespace NewsApp.Core.Interfaces
{
    public interface IApiService
    {
        /// <summary>
        /// Gets top story ids
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<int>> GetTopStoryIds();

        /// <summary>
        /// Gets single sotry item by identifier
        /// </summary>
        /// <param name="sotryId"></param>
        /// <returns></returns>
        Task<NewsItemModel?> GetStoryItem(int sotryId);
    }
}
