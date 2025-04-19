using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Stefanini.Infra.Services
{
    public sealed class CacheService(IMemoryCache cache, ILogger<CacheService> logger) : ICacheService
    {
        private static readonly MemoryCacheEntryOptions CACHE_OPTIONS = new()
        {
            SlidingExpiration = TimeSpan.FromHours(1),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
        };

        private readonly ConcurrentDictionary<string, byte> _cacheKeys = new();

        public Task SetAsync(string cacheTag, object cacheValue)
        {
            logger.LogInformation("Creating cache\n\tTag: {0}", cacheTag);

            if (cacheTag is null || cacheValue is null)
            {
                logger.LogWarning("Cache not created {0} {1} cannot be null", nameof(cacheTag), nameof(cacheValue));
                return Task.CompletedTask;
            }

            cache.Set(cacheTag, cacheValue, CACHE_OPTIONS);
            _cacheKeys.TryAdd(cacheTag, 0);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string cacheTag)
        {
            if (cacheTag is null)
                return Task.CompletedTask;

            logger.LogInformation("Removing cache\n\tTag: {0}", cacheTag);

            cache.Remove(cacheTag);
            _cacheKeys.TryRemove(cacheTag, out _);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(params string[] cacheTags)
        {
            foreach (var pattern in cacheTags)
            {
                var regex = WildcardToRegex(pattern);
                var keysToRemove = GetAllKeys().Where(k => Regex.IsMatch(k, regex)).ToList();

                foreach (var key in keysToRemove)
                {
                    cache.Remove(key);
                    _cacheKeys.TryRemove(key, out _);
                    logger.LogInformation("Cache removed by pattern\n\tTag: {0}", key);
                }
            }

            return Task.CompletedTask;
        }

        public Task<TResponse?> GetAsync<TResponse>(string cacheTag)
        {
            if (cacheTag is null)
                return Task.FromResult<TResponse?>(default);

            logger.LogInformation("Retrieving Cache\n\tTag: {0}", cacheTag);

            if (cache.TryGetValue(cacheTag, out TResponse? response))
            {
                logger.LogInformation("Retrieving Cache\n\tTag: {0}\n\tSuccess: {1}", cacheTag, true);
                return Task.FromResult(response);
            }

            logger.LogInformation("Retrieving Cache\n\tTag: {0}\n\tSuccess: {1}", cacheTag, false);
            return Task.FromResult<TResponse?>(default);
        }

        public async Task<TResponse?> GetOrSetAsync<TResponse>(string cacheTag, Func<TResponse?> fallbackFunc)
        {
            var cachedResponse = await GetAsync<TResponse>(cacheTag);

            if (cachedResponse is null)
            {
                var queryResponse = fallbackFunc();

                if (queryResponse is not null)
                    await SetAsync(cacheTag, queryResponse);

                return queryResponse;
            }

            return cachedResponse;
        }

        public async Task<TResponse?> GetOrSetAsync<TResponse>(string cacheTag, Func<ValueTask<TResponse>> fallbackFuncAsync)
        {
            var cachedResponse = await GetAsync<TResponse>(cacheTag);

            if (cachedResponse is null)
            {
                var queryResponse = await fallbackFuncAsync();

                if (queryResponse is not null)
                    await SetAsync(cacheTag, queryResponse);

                return queryResponse;
            }

            return cachedResponse;
        }

        public async Task<TResponse?> GetOrSetAsync<TResponse>(string cacheTag, Func<Task<TResponse>> fallbackFuncAsync)
        {
            var cachedResponse = await GetAsync<TResponse>(cacheTag);

            if (cachedResponse is null)
            {
                var queryResponse = await fallbackFuncAsync();

                if (queryResponse is not null)
                    await SetAsync(cacheTag, queryResponse);

                return queryResponse;
            }

            return cachedResponse;
        }

        private IEnumerable<string> GetAllKeys() => _cacheKeys.Keys;

        private static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                             .Replace("\\*", ".*")
                             .Replace("\\?", ".") + "$";
        }
    }
}
