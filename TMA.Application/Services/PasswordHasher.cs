using System.Security.Cryptography;

namespace TMA.Application.Services
{
    public static class PasswordHasher
    {
        private static readonly int SaltSize = 16; // 128 bits
        private static readonly int KeySize = 32; // 256 bits
        private static readonly int Iterations = 10000; // Adjust as needed

        public static string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(SaltSize); // Generate a unique salt

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);
                string newHash = Convert.ToBase64String(hash);
                return hashedPassword.Equals(newHash);
            }
        }
    }
}
