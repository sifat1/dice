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

    public int GenerateUniformRandomInt(int minValue, int maxValue)
    {
        uint range = (uint)(maxValue - minValue + 1);
        uint maxExclusive = uint.MaxValue - (uint.MaxValue % range + 1) % range;
        uint randomValue;
        
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[4];
            do
            {
                rng.GetBytes(randomBytes);
                randomValue = BitConverter.ToUInt32(randomBytes, 0);
            } while (randomValue >= maxExclusive);
        }
        return (int)(randomValue % range) + minValue;
    }

    public byte[] ComputeHmacSha3(byte[] key, int message)
    {
        byte[] messageBytes = BitConverter.GetBytes(message);
        using (var hmac = new HMACSHA3_256(key))
            return hmac.ComputeHash(messageBytes);
    }
}