using System;
using System.Security.Cryptography;

namespace ExpensesCounter.Web.BLL.Account
{
    internal class InvalidHashException : Exception
    {
        public InvalidHashException()
        {
        }

        public InvalidHashException(string message)
            : base(message)
        {
        }

        public InvalidHashException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    internal class CannotPerformOperationException : Exception
    {
        public CannotPerformOperationException()
        {
        }

        public CannotPerformOperationException(string message)
            : base(message)
        {
        }

        public CannotPerformOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    internal static class PasswordHasher
    {
        // These constants may be changed without breaking existing hashes.
        public const int SALT_BYTES        = 24;
        public const int HASH_BYTES        = 18;
        public const int PBKDF2_ITERATIONS = 64000;

        // These constants define the encoding and may not be changed.
        public const int HASH_SECTIONS        = 5;
        public const int HASH_ALGORITHM_INDEX = 0;
        public const int ITERATION_INDEX      = 1;
        public const int HASH_SIZE_INDEX      = 2;
        public const int SALT_INDEX           = 3;
        public const int PBKDF2_INDEX         = 4;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            var salt = new byte[SALT_BYTES];
            try
            {
                using (var csprng = new RNGCryptoServiceProvider())
                {
                    csprng.GetBytes(salt);
                }
            }
            catch (CryptographicException ex)
            {
                throw new CannotPerformOperationException(
                                                          "Random number generator not available.",
                                                          ex
                                                         );
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                                                          "Invalid argument given to random number generator.",
                                                          ex
                                                         );
            }

            var hash = Generate(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

            // format: algorithm:iterations:hashSize:salt:hash
            var parts = "sha1:"                      +
                        PBKDF2_ITERATIONS            +
                        ":"                          +
                        hash.Length                  +
                        ":"                          +
                        Convert.ToBase64String(salt) +
                        ":"                          +
                        Convert.ToBase64String(hash);
            return parts;
        }

        public static bool VerifyPassword(string password, string goodHash)
        {
            char[] delimiter = {':'};
            var    split     = goodHash.Split(delimiter);

            if (split.Length != HASH_SECTIONS)
                throw new InvalidHashException(
                                               "Fields are missing from the password hash."
                                              );

            // We only support SHA1 with C#.
            if (split[HASH_ALGORITHM_INDEX] != "sha1")
                throw new CannotPerformOperationException(
                                                          "Unsupported hash type."
                                                         );

            var iterations = int.Parse(split[ITERATION_INDEX]);

            var salt     = Convert.FromBase64String(split[SALT_INDEX]);
            var hash     = Convert.FromBase64String(split[PBKDF2_INDEX]);
            var testHash = Generate(password, salt, iterations, hash.Length);

            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff                                                = (uint) a.Length ^ (uint) b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++) diff |= (uint) (a[i] ^ b[i]);

            return diff == 0;
        }

        private static byte[] Generate(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}