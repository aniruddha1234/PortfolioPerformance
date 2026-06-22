using System.Collections.Concurrent;
using PortfolioPerformance.Api.Models.Responses;

namespace PortfolioPerformance.Api.Services;

public class IdempotencyService
{
    private readonly ConcurrentDictionary<string, AttributionResponse> _cache = new();

    public bool Exists(string requestId)
    {
        return _cache.ContainsKey(requestId);
    }

    public AttributionResponse Get(string requestId)
    {
        return _cache[requestId];
    }

    public void Save(string requestId, AttributionResponse response)
    {
        _cache.TryAdd(requestId, response);
    }
}