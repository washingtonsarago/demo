namespace GrupoNC.DemoProject.Api.Utilities.Extensions
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class String_Extenstions
    {
        private const string passPhrase = "12345678901234567890123456789012";

        public static Guid? ToGuid(this string content, Func<Guid?> defaultReturnAction)
        {
            if (string.IsNullOrEmpty(content)) return defaultReturnAction.Invoke();

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(content));
                return new Guid(hash);
            }
        }

        public static Guid?[] ToGuidArray(this string[] contentList, Func<Guid?[]> defaultReturnAction)
        {
            if (contentList.Length == 0) return defaultReturnAction.Invoke();

            var guidList = new List<Guid?>();

            foreach (var content in contentList)
            {
                guidList.Add(new Guid(content));
            }

            return guidList.ToArray();
        }

        public static string Encrypt(this string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passPhrase);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(this string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passPhrase);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

       public static string ToMD5(this string plainText)
        {
            string result;
            using (MD5 hash = MD5.Create())
            {
                result = String.Join
                (
                    "",
                    from ba in hash.ComputeHash
                    (
                        Encoding.UTF8.GetBytes(plainText)
                    )
                    select ba.ToString("x2")
                );
            }
            return result;
        }
    }
}