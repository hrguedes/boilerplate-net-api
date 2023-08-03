using System.Security.Cryptography;
using System.Text;
using Consts;

namespace Utils;

public static class EncryptionHelper
{
    public static string Encrypt(string value)
    {
        byte[] encryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(LaunchSettings.ENCRYPTION_KEY);
            aesAlg.Mode = CipherMode.ECB;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            encryptedBytes = encryptor.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string encryptedValue)
    {
        byte[] decryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(LaunchSettings.ENCRYPTION_KEY);
            aesAlg.Mode = CipherMode.ECB;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);
            decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}