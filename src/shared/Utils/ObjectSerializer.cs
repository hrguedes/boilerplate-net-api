using MessagePack;
using MessagePack.Resolvers;

namespace Utils;

public class ObjectSerializer
{
    public static string Serialize(object value)
    {
        MessagePackSerializer.SetDefaultResolver(ContractlessStandardResolver.Instance);
        byte[] inArray = MessagePackSerializer.Serialize(value);
        return Convert.ToBase64String(inArray);
    }

 

    public static T Deserialize<T>(string data)
    {
        MessagePackSerializer.SetDefaultResolver(ContractlessStandardResolver.Instance);
        byte[] bytes = Convert.FromBase64String(data);
        return MessagePackSerializer.Deserialize<T>(bytes);
    }
}