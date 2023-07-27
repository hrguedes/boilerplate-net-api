using Consts;
using StackExchange.Redis;
using Utils;

namespace RedisCache;

public class RedisDataCache
{
    private ConnectionMultiplexer redis;
    private string keyPrefix;
    
    public RedisDataCache()
    {
        var configurationOptions = new ConfigurationOptions
        {
            KeepAlive = Convert.ToInt32(TimeSpan.FromSeconds(LaunchSettings.KeepAlive).TotalMilliseconds),
            AsyncTimeout = Convert.ToInt32(TimeSpan.FromSeconds(LaunchSettings.ResponseTimeout).TotalMilliseconds),
            ConnectTimeout = Convert.ToInt32(TimeSpan.FromSeconds(LaunchSettings.ConnectTimeout).TotalMilliseconds),
            ConnectRetry = 2,
            ReconnectRetryPolicy = new LinearRetry(100)
        };
       var endpoints = LaunchSettings.RedisDatabases;
        foreach (var hostAndPort in endpoints)
        {
            configurationOptions.EndPoints.Add(hostAndPort);
        }
        
        redis = ConnectionMultiplexer.Connect(configurationOptions);
        keyPrefix = LaunchSettings.KeyPrefix;
    }


    public bool GetValue(string key, out string? value)
    {
        var connection = ConnectionMultiplexer.Connect(LaunchSettings.RedisConnectionString);
        var db = connection.GetDatabase();
        var redisValue = db.StringGet(key);

        if (redisValue.HasValue)
        {
            value = redisValue.ToString();
            return true;
        }

        value = null;
        return false;
    }


    public async Task DependencyUpdateAsync(string key)
    {
        IDatabase db = GetDatabase();
        await db.StringSetAsync(await BuildKey(db, key), Guid.NewGuid().ToString());
    }


    public async Task<T> GetAsync<T>(string key, Func<Task<T>> hydrate = null, TimeSpan? expires = null,
        List<string> dependencies = null)
    {
        string value = await GetAsync(key, dependencies);
        if (value == null && hydrate != null)
        {
            T data = await hydrate();
            if (data == null)
            {
                return default(T);
            }


            if (!expires.HasValue)
            {
                expires = DefaultTTL();
            }


            await SetAsync(key, data, expires.Value, dependencies);
            return data;
        }


        if (value == null)
        {
            return default(T);
        }
        return ObjectSerializer.Deserialize<T>(value);
    }


    public async Task<string> GetAsync(string key, List<string> dependencies = null)
    {
        IDatabase db = GetDatabase();
        return await db.StringGetAsync(await BuildKey(db, key, dependencies));
    }


    public async Task<string> BuildKey(IDatabase db, string key, List<string> dependencies = null)
    {
        string output = keyPrefix + key;
        if (dependencies == null)
        {
            return output;
        }


        List<Task<string>> dependency_values = new List<Task<string>>();
        foreach (string d in dependencies)
        {
            dependency_values.Add(GetAsync(d));
        }


        await Task.WhenAll(dependency_values.ToArray());
        foreach (Task<string> i in dependency_values)
        {
            if (!string.IsNullOrEmpty(i.Result))
            {
                output = output + "-" + i.Result;
            }
        }


        return output;
    }


    public IDatabase GetDatabase()
    {
        return redis.GetDatabase();
    }


    public async Task RemoveAsync(string key, List<string> dependencies = null)
    {
        IDatabase db = GetDatabase();
        await db.KeyDeleteAsync(await BuildKey(db, key, dependencies));
    }


    public async Task SetAsync(string key, object data, TimeSpan? expires = null, List<string> dependencies = null)
    {
        IDatabase db = GetDatabase();
        string string_value = ObjectSerializer.Serialize(data);
        string data_key = await BuildKey(db, key, dependencies);
        if (!expires.HasValue)
        {
            expires = DefaultTTL();
        }


        await db.StringSetAsync(data_key, string_value, expires);
    }


    private TimeSpan DefaultTTL()
    {
        return TimeSpan.FromMinutes(30.0);
    }
}