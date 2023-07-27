namespace Consts;

public static class LaunchSettings
{
    #region Mongo DB
    public static string ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";
    public static string Database = "BOILERPLATEPROJECT";
    #endregion

    #region Redis
    public static string RedisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";
    public static string[] RedisDatabases = {"BOILERPLATEPROJECT"};
    public static double KeepAlive = 10;
    public static double ResponseTimeout = 10;
    public static double ConnectTimeout = 10;
    public static string KeyPrefix = "KEY_BOILERPLATE";
    
    #endregion
}