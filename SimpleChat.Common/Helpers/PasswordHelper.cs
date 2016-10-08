using DevOne.Security.Cryptography.BCrypt;

namespace SimpleChat.Common.Helpers
{
    public class PasswordHelper
    {
        public static string GetPasswordHash(string password)
        {
            string salt = BCryptHelper.GenerateSalt();

            return BCryptHelper.HashPassword(password, salt);
        }

        public static bool IsPasswordHashValid(string hash, string password)
        {
            return BCryptHelper.CheckPassword(password, hash);
        }
    }
}