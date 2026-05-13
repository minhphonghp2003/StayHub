namespace StayHub.Application.Services
{
    public interface IRedisCacheService
    {

        Task SetAsync<T>(string key, T value);

        Task<T?> GetAsync<T>(string key);

        Task RemoveAsync(string key);
    }
}
