using System;
using System.Security.Cryptography;

public class CryptographicService
{
    private const int KeySizeBytes = 32; // 256 bits

    public (byte[] Key, int Number, string Hmac) GenerateCommitment(int minValue, int maxValue)
    {
        var key = GenerateSecretKey();
        var number = GenerateUniformRandomInt(minValue, maxValue);
        var hmac = ComputeHmacSha3(key, number);
        return (key, number, BitConverter.ToString(hmac).Replace("-", ""));
    }

    public byte[] GenerateSecretKey()
    {
        byte[] key = new byte[KeySizeBytes];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(key);
        return key;
    }

    public int GenerateUniformRandomInt(int minValue, int maxValue) => RandomNumberGenerator.GetInt32(minValue, maxValue + 1);


    public byte[] ComputeHmacSha3(byte[] key, int message)
    {
        byte[] messageBytes = BitConverter.GetBytes(message);
        using (var hmac = new HMACSHA3_256(key))
            return hmac.ComputeHash(messageBytes);
    }
}