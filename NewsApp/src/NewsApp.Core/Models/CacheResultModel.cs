using NewsApp.Core.Enums;
using System.Diagnostics.CodeAnalysis;

namespace NewsApp.Core.Models
{
    /// <summary>
    /// Cache result model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CacheResultModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="key">key</param>
        public CacheResultModel(string key)
        {
            CacheKey = key;
            CacheValue = string.Empty;
            CacheStatus = CacheStatusOption.ResultPending;
            Error = null;
        }

        /// <summary>
        /// Gets or sets the cache key.
        /// </summary>
        /// <value>
        /// Cache key
        /// </value>
        public string CacheKey { get; set; }

        /// <summary>
        /// Gets or sets the cache value
        /// </summary>
        /// <value>
        /// Cache value.
        /// </value>
        public object CacheValue { get; set; }

        /// <summary>
        /// Gets or sets the cache status
        /// </summary>
        /// <value>
        /// Cache status
        /// </value>
        public CacheStatusOption CacheStatus { get; set; }

        /// <summary>
        /// Gets or sets the error
        /// </summary>
        /// <value>
        /// Error
        /// </value>
        public Exception Error { get; set; }
    }
}