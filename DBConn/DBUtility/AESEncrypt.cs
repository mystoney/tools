using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
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
            /// <summary>
            /// AES加密
            /// </summary>
            /// <param name="aesModel"></param>
            /// <returns></returns>
            public static byte[] Encrypt(AesModel aesModel)
            {
                //使用32位密钥
                byte[] key32 = new byte[32];
                //如果我们的密钥不是32为，则自动补全到32位
                byte[] byteKey = Encoding.UTF8.GetBytes(aesModel.Key.PadRight(key32.Length));
                //复制密钥
                Array.Copy(byteKey, key32, key32.Length);

                //使用16位向量
                byte[] iv16 = new byte[16];
                //如果我们的向量不是16为，则自动补全到16位
                byte[] byteIv = Encoding.UTF8.GetBytes(aesModel.IV.PadRight(iv16.Length));
                //复制向量
                Array.Copy(byteIv, iv16, iv16.Length);

                // 创建加密对象,Rijndael 算法
                //Rijndael RijndaelAes = Rijndael.Create();
                RijndaelManaged RijndaelAes = new RijndaelManaged();
                RijndaelAes.Mode = aesModel.Mode;
                RijndaelAes.Padding = aesModel.Padding;
                RijndaelAes.Key = key32;
                RijndaelAes.IV = iv16;
                byte[] result = null;
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream EncryptStream = new CryptoStream(ms, RijndaelAes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            EncryptStream.Write(aesModel.Data, 0, aesModel.Data.Length);
                            EncryptStream.FlushFinalBlock();
                            result = ms.ToArray();
                        }
                    }
                }
                catch { }
                return result;
            }

            /// <summary>
            /// AES解密
            /// </summary>
            /// <param name="aesModel"></param>
            /// <returns></returns>
            public static byte[] Decrypt(AesModel aesModel)
            {
                //使用32位密钥
                byte[] key32 = new byte[32];
                //如果我们的密钥不是32为，则自动补全到32位
                byte[] byteKey = Encoding.UTF8.GetBytes(aesModel.Key.PadRight(key32.Length));
                //复制密钥
                Array.Copy(byteKey, key32, key32.Length);

                //使用16位向量
                byte[] iv16 = new byte[16];
                //如果我们的向量不是16为，则自动补全到16位
                byte[] byteIv = Encoding.UTF8.GetBytes(aesModel.IV.PadRight(iv16.Length));
                //复制向量
                Array.Copy(byteIv, iv16, iv16.Length);

                // 创建解密对象,Rijndael 算法
                //Rijndael RijndaelAes = Rijndael.Create();
                RijndaelManaged RijndaelAes = new RijndaelManaged();
                RijndaelAes.Mode = aesModel.Mode;
                RijndaelAes.Padding = aesModel.Padding;
                RijndaelAes.Key = key32;
                RijndaelAes.IV = iv16;
                byte[] result = null;
                try
                {
                    using (MemoryStream ms = new MemoryStream(aesModel.Data))
                    {
                        using (CryptoStream DecryptStream = new CryptoStream(ms, RijndaelAes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (MemoryStream msResult = new MemoryStream())
                            {
                                byte[] temp = new byte[1024 * 1024];
                                int len = 0;
                                while ((len = DecryptStream.Read(temp, 0, temp.Length)) > 0)
                                {
                                    msResult.Write(temp, 0, len);
                                }

                                result = msResult.ToArray();
                            }
                        }
                    }
                }
                catch { }
                return result;
            }

            /// <summary>
            /// AES加密字符串
            /// </summary>
            /// <param name="data"></param>
            /// <param name="key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static string Encrypt(string data, string key, string iv = "")
            {
                
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] result = Encrypt(new AesModel
                {
                    Data = bytes,
                    Key = key,
                    IV = iv,
                    Mode = CipherMode.CFB,
                    Padding = PaddingMode.PKCS7
                });
                if (result == null)
                {
                    return "";
                }
                return Convert.ToBase64String(result);
            }

            /// <summary>
            /// AES解密字符串
            /// </summary>
            /// <param name="data"></param>
            /// <param name="key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static string Decrypt(string data, string key, string iv = "")
            {
                

                byte[] bytes = Convert.FromBase64String(data);
                byte[] result = Decrypt(new AesModel
                {
                    Data = bytes,
                    Key = key,
                    IV = iv,
                    Mode = CipherMode.CFB,
                    Padding = PaddingMode.PKCS7
                });
                if (result == null)
                {
                    return "";
                }
                return Encoding.UTF8.GetString(result);
            }

        /// <summary>
        /// 自定义Base16编码
        /// </summary>
        /// <param name="str">需要编码的字符串</param>
        /// <param name="autoCode">自定义Base16编码数组,16个元素,可以为数字、字符、特殊符号,若不填,使用默认的Base16编码数组,解码与编码的Base16编码数组一样</param>
        /// <returns></returns>
        public static string AutoBase16Encrypt(string str, string[] autoCode)
        {
            string innerStr = string.Empty;
            StringBuilder strEn = new StringBuilder();
            if (autoCode == null || autoCode.Length < 16)
                autoCode = new string[] { "a", "2", "B", "g", "E", "5", "f", "6", "C", "8", "o", "9", "Z", "p", "k", "M" };
            System.Collections.ArrayList arr = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(str));
            for (int i = 0; i < arr.Count; i++)
            {
                byte data = (byte)arr[i];
                int v1 = data >> 4;
                strEn.Append(autoCode[v1]);
                int v2 = ((data & 0x0f) << 4) >> 4;
                strEn.Append(autoCode[v2]);
            }
            return strEn.ToString();
        }

        public class AesModel
        {
            /// <summary>
            /// 需要加密/解密的数据
            /// </summary>
            public byte[] Data { get; set; }

            /// <summary>
            /// 密钥
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// 向量
            /// </summary>
            public string IV { get; set; }

            /// <summary>
            /// 加密模式
            /// </summary>
            public CipherMode Mode { get; set; }

            /// <summary>
            /// 填充模式
            /// </summary>
            public PaddingMode Padding { get; set; }
        }

    }    
}
