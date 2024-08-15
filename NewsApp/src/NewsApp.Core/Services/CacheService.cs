using Microsoft.Extensions.Caching.Memory;
using NewsApp.Core.Enums;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;

namespace NewsApp.Core.Services
{
    /// <summary>
    /// Cache Service
    /// </summary>
    public class CacheService : ICacheService
    {
        private CacheResultModel result;
        private IMemoryCache _cache;



        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cache">cache</param>
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        #region RETRIEVE

        /// <summary>
        /// Retrieves from cache asynchronously
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<CacheResultModel> RetrieveFromCacheAsync(string key)
        {
            result = new CacheResultModel(key);
            try
            {
                await Task.Run(() =>
                {
                    if (_cache.Get(key) == null)
                    {
                        result.CacheStatus = CacheStatusOption.DoesNotExists;
                    }
                    else
                    {
                        result.CacheStatus = CacheStatusOption.Exists;
                        result.CacheValue = _cache.Get(key);
                    }
                });
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }

        #endregion

        #region SAVE

        /// <summary>
        /// Saves to cache asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="objectToCache">object to cache</param>
        /// <param name="expirationTimeLimit">expiration time limit</param>
        /// <returns></returns>
        public async Task<CacheResultModel> SaveToCacheAsync<T>(string key, T objectToCache, int? expirationTimeLimit = null)
        {
            result = new CacheResultModel(key);
            object cacheObject = null;
            try
            {
                await Task.Run(() =>
                {
                    if (!_cache.TryGetValue(key, out cacheObject))
                    {
                        cacheObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                        var memoryCacheEntryOptions = new MemoryCacheEntryOptions();

                        memoryCacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTimeLimit ?? 180));

                        _cache.Set(key, objectToCache, memoryCacheEntryOptions);
                    }
                });
                result.CacheValue = cacheObject;
                result.CacheStatus = CacheStatusOption.Cached;
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }

        #endregion

        #region CacheExpired

        /// <summary>
        /// Caches the expired callback
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="reason">reason</param>
        /// <param name="state">state</param>
        private async void CacheExpired_Callback(object key, object value, EvictionReason reason, object state)
        {
            var existingDataInCache = await RetrieveFromCacheAsync(key.ToString());

            if (existingDataInCache.CacheStatus == CacheStatusOption.DoesNotExists)
            {
                await SaveToCacheAsync(key.ToString(), value);
            }
        }

        #endregion
    }
}