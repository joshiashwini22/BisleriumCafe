using System.Security.Cryptography;

namespace BisleriumCafe.Data.Utils
{
    public static class Hasher
    {
        private const char segmentedDelimiter = ':';

        public static string HashedSecret(string input)
        {
            var saltSize = 16;
            var iterations = 100_100;
            var keySize = 32;
            HashAlgorithmName algo = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algo, keySize);

            return string.Join(
                segmentedDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algo
                );
        }

        public static bool VerifyHashedSecret(string input, string hashedString)
        {
            string[] segments = hashedString.Split(segmentedDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algo = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algo,
                hash.Length
               );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }

}