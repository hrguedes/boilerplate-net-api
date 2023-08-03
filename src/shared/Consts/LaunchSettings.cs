namespace Consts;

public static class LaunchSettings
{
    #region Mongo DB

    public static string MONGO_CONNECTION_STRING =
        Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";

    public static string NOME_BANCO_BASE = Environment.GetEnvironmentVariable("NOME_BANCO_BASE") ?? "IOB_BASE";

    #endregion

    #region Redis

    public static string REDIS_CONNECTION_STRING =
        Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";

    public static string[] REDIS_DATABASES =
    {
        "BOILERPLATE_REDIS_DB"
    };

    public static string KEEP_ALIVE = Environment.GetEnvironmentVariable("KEEP_ALIVE") ?? "10";
    public static string RESPONSE_TIMEOUT = Environment.GetEnvironmentVariable("RESPONSE_TIMEOUT") ?? "10";
    public static string CONNECT_TIMEOUT = Environment.GetEnvironmentVariable("CONNECT_TIMEOUT") ?? "10";
    public static string KEY_PREFIXO = Environment.GetEnvironmentVariable("KEY_PREFIXO") ?? "BOILERPLATE_CACHE";

    #endregion

    #region Chaves

    public static string JWT_SECRET =
        Environment.GetEnvironmentVariable("JWT_SECRET") ?? "fedaf7d8863b48e197b9287d492b708e";

    public static string ENCRYPTION_KEY =
        Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "fedaf7d8863b48e197b9287d492b908e";

    public static string SENHA_PADRAO = Environment.GetEnvironmentVariable("SENHA_PADRAO") ?? "ADMIN123";

    #endregion

    #region Configuracoes de Sessao

    public static string TEMPO_SESSAO_HORAS = Environment.GetEnvironmentVariable("TEMPO_SESSAO_HORAS");

    #endregion
}