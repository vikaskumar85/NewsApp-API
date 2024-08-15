using NewsApp.Core.Models;

namespace NewsApp.Core.Interfaces
{
    /// <summary>
    /// Cache Service Interface
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Saves to cache asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="objectToCache">object to cache</param>
        /// <param name="expirationTimeLimit">expiration time limit</param>
        /// <returns></returns>
        Task<CacheResultModel> SaveToCacheAsync<T>(string key, T objectToCache, int? expirationTimeLimit = null);



        /// <summary>
        /// Retrieves from cache asynchronously
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<CacheResultModel> RetrieveFromCacheAsync(string key);
    }
}