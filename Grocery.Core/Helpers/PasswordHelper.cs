using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Grocery.Core.Helpers
{
    public static class PasswordHelper {
        private static readonly Regex PasswordRegex = new(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            RegexOptions.Compiled);
        public static string HashPassword(string password) {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, 100000, HashAlgorithmName.SHA256, 32);
            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
        }

        public static bool CheckPasswords(string password, string secondPassword) {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(secondPassword))
                return false;
            if (!IsValidPassword(password))
                return false;

            return password == secondPassword;
        }

        public static bool IsValidPassword(string password) {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return PasswordRegex.IsMatch(password);
        }

        public static bool VerifyPassword(string password, string storedHash) {
            var parts = storedHash.Split('.');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var inputHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, 100000, HashAlgorithmName.SHA256, 32);

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }
}
