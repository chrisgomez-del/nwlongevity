using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;


namespace Innovation.Areas.Innovation.Infrastructure.Caching
{
    public class MemoryCacheClient : ICacheClient
    {
        private readonly ObjectCache cache = MemoryCache.Default;

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="SlidingExpirationMinutes">The sliding expiration minutes.</param>
        public void Add(string key, object value, int SlidingExpirationMinutes = 1440)
        {
            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, SlidingExpirationMinutes, 0) };
            cache.Add(key, value, policy);
        }

        /// <summary>
        /// Determines if the cache exists.
        /// </summary>
        /// <param name="cacheName">Name of the cache.</param>
        /// <returns></returns>
        public bool Exists(string cacheName)
        {
            return (cache.Get(cacheName) != null);
        }

        /// <summary>
        /// Clears all the caches from memory.
        /// </summary>
        public void FlushAll()
        {
            var allKeys = cache.Select(kvp => kvp.Key).ToList();

            foreach (var key in allKeys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllKeys()
        {
            return cache.Select(kvp => kvp.Key).ToList();
        }

        /// <summary>
        /// Gets or creates the <see cref="ICacheClient"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="createItem">The create item.</param>
        /// <param name="SlidingExpirationMinutes">The sliding expiration minutes.</param>
        /// <returns></returns>
        public T GetOrCreate<T>(string key, Func<T> createItem, int SlidingExpirationMinutes = 1440)
        {
            T cacheItem;
            var itemFromCache = cache.Get(key);

            if (itemFromCache == null)
            {
                cacheItem = createItem();
                if (cacheItem == null) return default(T);

                var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, SlidingExpirationMinutes, 0) };

                cache.Add(key, cacheItem, policy);
            }
            else
            {
                cacheItem = (T)itemFromCache;
            }

            return cacheItem;
        }

        /// <summary>
        /// Removes the cache with the specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public void Remove(string key)
        {
            if (this.Exists(key)) { cache.Remove(key); }
        }

        /// <summary>
        /// Removes caches based on a partial key.
        /// </summary>
        /// <param name="partialKey">The partial key.</param>
        public void RemoveBulk(string partialKey)
        {
            var allKeys = cache.Select(kvp => kvp.Key).Where(x => x.Contains(partialKey)).ToList();

            foreach (var key in allKeys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Replaces the specified cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="SlidingExpirationMinutes">The sliding expiration minutes.</param>
        public void Replace<T>(string key, T item, int SlidingExpirationMinutes = 1440)
        {
            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, SlidingExpirationMinutes, 0) };

            cache.Set(key, item, policy);
        }
    }
}
