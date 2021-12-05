using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRApp.Common
{
    public class PasswordHasher : IPasswordHasher, IDisposable
    {
        private readonly Encoding _encoding;
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly RandomNumberGenerator _rng;

        public PasswordHasher()
        {
            _encoding = Encoding.UTF8;
            _hashAlgorithm = new SHA256Managed();
            _rng = new RNGCryptoServiceProvider();
        }


        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(byte[] b1, byte[] b2, long count);

        public byte[] ComputeSalt()
        {
            var saltLength = RandomNumberGenerator.GetInt32(3, 6);
             var saltBytes = new byte[saltLength];
            _rng.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }

        public byte[] ComputeHash(string plainText, byte[] salt)
        {
            var plainTextBytes = _encoding.GetBytes(plainText);

            var plainTextWithSaltBytes =
                new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return _hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public bool Compare(byte[] b1, byte[] b2)
        {
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
            _rng.Dispose();
        }
    }
}
