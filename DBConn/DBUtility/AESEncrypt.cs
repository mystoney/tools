using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBConn.DBUtility
{
    public class AESEncrypt
    {
        public AESEncrypt()
        {

        }
        //AES加密
        public static string AesEncrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
            if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
            if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
            if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Encoding.UTF8.GetBytes(value);
            using (var aes = new RijndaelManaged())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var cryptoTransform = aes.CreateEncryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        //AES解密
        public static string AesDecrypt(string value, string key, string iv)
        {
            //string avalue = "edff48a59bea845b2edb0d9de5dcfe501a26b686f6606f59b3b2880adae1d28bac794829cb458d08c41cecf6a50604c0b9de64b34b8461163d7922b2f66ba569";
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
            if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
            if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
            if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Convert.(value);
            //var _valueByte = Encoding.UTF8.GetBytes(value);
            using (var aes = new RijndaelManaged())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.CFB;
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 256;
                aes.BlockSize = 24;
                var cryptoTransform = aes.CreateDecryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
        }
        //使用AES256解密字符串
        public static string DecryptAES256(string encryptedText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);


                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }



        //private byte[] aesKey;
        //private byte[] aesIV;

        //public void DecryptionService(string aesKeyHex, string aesIVHex)
        //{
        //    aesKey = HexToByteArray(aesKeyHex);
        //    aesIV = HexToByteArray(aesIVHex);
        //}

        //public static string AESDecrypt(string encryptedHex, byte[] aesKey, byte[] aesIV)
        //{
        //    byte[] encryptedBytes = System.Text.Encoding.Default.GetBytes("efcf72a487f9855214d40286e7eef451399d8d0b4e8c10394bf508d1b83a4d4c1df069e769851d06edd2c54b094388e810b131f47c853807ef578bd097cc69ecc8fbd8e5b80c6095fa06030f47036fdb929e73c428f530b7751bc66d9eb99bc035a0006af81eb3d2e08774d549");

        //    //string key = "5a75d5ec839a8f1ed686f0ddb67d5f09";
        //    //string iv = "f244ef6f0accec87";
        //    //byte[] key1 = System.Text.Encoding.Default.GetBytes(key);
        //    //byte[] iv1 = System.Text.Encoding.Default.GetBytes(iv);


        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = aesKey;
        //        aesAlg.IV = aesIV;
        //        aesAlg.Mode = CipherMode.CFB; // CFB模式
        //        aesAlg.Padding = PaddingMode.PKCS7; // 使用PKCS7填充

        //        using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
        //        using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
        //        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //        {
        //            string decryptedText = srDecrypt.ReadToEnd();
        //            return decryptedText;
        //        }
        //    }
        //}

        //public static byte[] HexToByteArray(string hex)
        //{
        //    int byteCount = hex.Length / 2;
        //    byte[] byteArray = new byte[byteCount];

        //    for (int i = 0; i < byteCount; i++)
        //    {
        //        byteArray[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        //    }

        //    return byteArray;
        //}







    }
}
