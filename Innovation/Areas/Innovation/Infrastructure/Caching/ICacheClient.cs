using System;
using System.Collections.Generic;
 

namespace Innovation.Areas.Innovation.Infrastructure.Caching
{
    public interface ICacheClient
    {
        /// <summary>
        /// Determines if the cache exists.
        /// </summary>
        /// <param name="key">the cache key.</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// Clears all the caches from memory.
        /// </summary>
        void FlushAll();

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// Gets or creates the <see cref="ICacheClient"/>.
        /// </summary>
        /// <typeparam name="T">Implements interface <see cref="ICacheClient"/>.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="item">The item.</param>
        /// <param name="SlidingExpirationMinutes">The sliding expiration minutes.</param>
        /// <returns></returns>
        T GetOrCreate<T>(string key, Func<T> item, int SlidingExpirationMinutes = 1440);

        /// <summary>
        /// Removes the cache with the specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        void Remove(string key);

        /// <summary>
        /// Removes caches based on a partial key.
        /// </summary>
        /// <param name="partialKey">The partial key.</param>
        void RemoveBulk(string partialKey);
    }
}
