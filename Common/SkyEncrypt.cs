using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Common
{
    public class SkyEncrypt
    {
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <returns>返回40位UTF8 大写</returns>  
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        //16字节,128位
        public static string MD5(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] byteArr = MD5.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        //20字节,160位
        public static string SHA128(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteArr = SHA1.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        //32字节,256位
        public static string SHA256(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider();
            byte[] byteArr = SHA256.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        //48字节,384位
        public static string SHA384(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA384CryptoServiceProvider SHA384 = new SHA384CryptoServiceProvider();
            byte[] byteArr = SHA384.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }

        //64字节,512位
        public static string SHA512(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider();
            byte[] byteArr = SHA512.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }

        /// <summary>
        /// HMAC-SHA1加密算法
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="input">要加密的串</param>
        /// <returns></returns>
        public static string HmacSha1(string key, string input)
        {
            byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);
            byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(input);
            HMACSHA1 hmac = new HMACSHA1(keyBytes);
            byte[] hashBytes = hmac.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
