using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace net5test.Common.Cryptography
{ 
   public class Cryptography: ICryptography
    {
        private static readonly string encryption_key = "123456789asdfghjkl";
        //加密密碼
        public   string encryptpwd(string password)
        {
            string phpMD5EncrptyKey = MD5ForPHP(encryption_key);
            byte[] phpMD5EncrptyKeybyte = Encoding.ASCII.GetBytes(phpMD5EncrptyKey);

            var (correctencryptPwd, iv) = encrypt(password, phpMD5EncrptyKeybyte);
            byte[] combinationOfPwdAndIV = new byte[correctencryptPwd.Length + iv.Length];
            Array.Copy(iv, 0, combinationOfPwdAndIV, 0, iv.Length);
            Array.Copy(correctencryptPwd, 0, combinationOfPwdAndIV, iv.Length, correctencryptPwd.Length);

            var correctencryptPwdAddNoise = AddCypherNoise(combinationOfPwdAndIV, phpMD5EncrptyKey);
            var encryptPassword = Convert.ToBase64String(correctencryptPwdAddNoise);
            return encryptPassword;
        }
        //解密
        public string decryptpwd(string encryptPwd)
        {
            string phpMD5EncrptyKey = MD5ForPHP(encryption_key);

            byte[] cypherData = Convert.FromBase64String(encryptPwd);

            var ans = RemoveCypherNoise(cypherData, phpMD5EncrptyKey);
            int anslengh = ans.Length;
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                iv[i] = ans[i];
            }
            byte[] sourceenprtyped = ans.Skip(16).ToArray();

            byte[] phpMD5EncrptyKeybyte = Encoding.ASCII.GetBytes(phpMD5EncrptyKey);
            var result = Decrypt(sourceenprtyped, phpMD5EncrptyKeybyte, iv);
            result = result.Replace("\0", string.Empty);

            return result;
        }

        #region private methods
        //AES256加密
        private (byte[] encryptPwd, byte[] iv) encrypt(string toEncrypt, byte[] encrptyKey)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize =128;
            aes.Key = encrptyKey;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return (resultArray, aes.IV);
        }
        //AES256解密
        private string Decrypt(byte[] toDecrypt, byte[] key, byte[] iv)
        {
            byte[] keyArray = key;
            byte[] ivArray = iv;
            byte[] toEncryptArray = toDecrypt;

            var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Key = keyArray;
            aes.IV = ivArray;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            ICryptoTransform cTransform = aes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        //MD5雜湊演算法
        private  string MD5ForPHP(string stringToHash)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] emailBytes = Encoding.UTF8.GetBytes(stringToHash.ToLower());
            byte[] hashedEmailBytes = md5.ComputeHash(emailBytes);
            StringBuilder sb = new StringBuilder();
            foreach (var b in hashedEmailBytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        //加密noise
        private  byte[] AddCypherNoise(byte[] cypherDataString, string keyString)
        {
            string keyStringsha1hash = sha1Hash(keyString);
            byte[] keyHash = Encoding.ASCII.GetBytes(keyStringsha1hash);
            byte[] result = new byte[cypherDataString.Length];
            for (int i = 0, j = 0; i < cypherDataString.Length; i++, j++)
            {
                if (j >= keyHash.Length)
                {
                    j = 0;
                }

                result[i] = (byte)((cypherDataString[i] + keyHash[j]) % 256);
            }
            return result;
        }
        //移除noise
        private byte[] RemoveCypherNoise(byte[] cypherDataString, string keyString)
        {
            int temp;

            string keyStringsha1hash = sha1Hash(keyString);
            byte[] keyHash = Encoding.ASCII.GetBytes(keyStringsha1hash);
            byte[] cypherData = cypherDataString;

            byte[] result = new byte[cypherData.Length];

            for (int i = 0, j = 0; i < cypherData.Length; i++, j++)
            {
                if (j >= keyHash.Length)
                {
                    j = 0;
                }

                temp = cypherData[i] - keyHash[j];
                if (temp < 0)
                {
                    temp = temp + 256;
                }

                result[i] = (byte)temp;
            }
            return result;
        }
        private  string sha1Hash(string password)
        {
            return string.Join("", SHA1CryptoServiceProvider.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("x2")));
        }
        #endregion
    }
}
