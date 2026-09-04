using BLL.Interfaces;
using System.Security.Cryptography;

namespace BLL.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%";
        private const int Length = 12;

        public string Generate()
        {
            return RandomNumberGenerator.GetString(Chars, Length);
        }
    }
}
