using System;
using System.Security.Cryptography;

namespace BaiTap4.Shared
{
    public static class PasswordHasher
    {
        public const int Iterations = 100000;
        public static byte[] NewSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            return salt;
        }

        public static byte[] Hash(string password, byte[] salt, int iterations)
        {
            using (var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return derive.GetBytes(32);
        }

        public static bool Verify(string password, byte[] salt, byte[] expected, int iterations)
        {
            byte[] actual = Hash(password, salt, iterations);
            int difference = actual.Length ^ expected.Length;
            for (int i = 0; i < actual.Length && i < expected.Length; i++) difference |= actual[i] ^ expected[i];
            Array.Clear(actual, 0, actual.Length);
            return difference == 0;
        }
    }
}
