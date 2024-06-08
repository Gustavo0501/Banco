using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Banco.SystemContext
{
    public static class Security
    {
        public static byte[] GenerateSalt(int size = 32)
        {
            byte[] salt = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static byte[] GenerateHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Combina a senha e o salt
                byte[] saltedPassword = Combine(Encoding.UTF8.GetBytes(password), salt);

                // Gera o hash da senha combinada com o salt
                return sha256.ComputeHash(saltedPassword);
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] combined = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
            return combined;
        }
    }
}
