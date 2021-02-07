using Corex.CipherEncryption.Infrastructure;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Corex.CipherEncryption.Derived.SHA256
{
    public abstract class BaseSHA256CipherEncryption : ICipherEncryption
    {
        public virtual string Decrypt(string text, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);
            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        public virtual string Encrypt(string text, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
            string resultValue = Convert.ToBase64String(bytesEncrypted);
            return resultValue;
        }
        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}
