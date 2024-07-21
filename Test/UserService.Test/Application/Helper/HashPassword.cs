

namespace UserService.Test.Application.Helper
{
    public static class HashPassword
    {
        private const int workFactor = 12;

        public static string CreatePasswordHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, workFactor);

        public static bool VerifyPasswordHash(string submitedPassword, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(submitedPassword, passwordHash);
    }
}
