using System;
using System.Threading.Tasks;

namespace Motocycle.Application.Services;

public interface ICacheService
{
    /// <summary>Deafult: 24hours</summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry">In seconds</param>
    Task SetAsync<T>(string key, T value, int? expiry);
    Task SetAsync<T>(string key, T value, TimeSpan expiry);
    Task<T> GetAsync<T>(string key);
    Task DeleteAsync(string key = null, string[] arrayOfKeys = null);
}