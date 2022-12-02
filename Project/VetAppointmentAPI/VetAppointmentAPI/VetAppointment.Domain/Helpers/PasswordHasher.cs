using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace VetAppointment.Domain.Helpers
{
    public class PasswordHasher
    {
        private static int saltLengthLimit = 32;
        private static byte[] GetSalt(int maximumSaltLength)
        {
            return RandomNumberGenerator.GetBytes(maximumSaltLength);
        }
        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        public static string GetHashedPassword(string password)
        {
            byte[] passwordAsByteArray = Encoding.ASCII.GetBytes(password);
            Argon2i argon2 = new Argon2i(passwordAsByteArray);

            byte[] salt = GetSalt();

            argon2.DegreeOfParallelism = 16;
            argon2.MemorySize = 8192;
            argon2.Iterations = 40;
            argon2.Salt= salt;

            byte[] hash = argon2.GetBytes(128);
            string result = Convert.ToBase64String(hash);
            return $"{Convert.ToBase64String(salt)}${result}";
        }

        public static bool IsPasswordValid(string hash, string password)
        {
            var pieces = hash.Split(new[] { '$' }, 2);
            if (pieces.Length < 2)
                return false;

            byte[] salt = Convert.FromBase64String(pieces[0]);

            string initialHash = pieces[1];

            byte[] passwordToHash = Encoding.ASCII.GetBytes(password);

            Argon2i argon2 = new Argon2i(passwordToHash);
            
            argon2.DegreeOfParallelism = 16;
            argon2.MemorySize = 8192;
            argon2.Iterations = 40;
            argon2.Salt = salt;

            byte[] hashToVerify = argon2.GetBytes(128);

            return (initialHash == Convert.ToBase64String(hashToVerify));
        }
    }
}
