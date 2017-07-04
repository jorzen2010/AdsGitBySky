using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;

namespace Common
{
    public class CommonTools
    {
        public static string ReplaceText(string userName, string content, string myName)
        {

            string path = string.Empty;

            path = System.Web.HttpContext.Current.Server.MapPath("\\EmailTemplate\\EmailTemplate.html");

            if (path == string.Empty)
            {

                return string.Empty;

            }


            System.IO.StreamReader sr = new System.IO.StreamReader(path);

            string str = string.Empty;

            str = sr.ReadToEnd();

            str = str.Replace("$USER_NAME$", userName);

            str = str.Replace("$CONTENT$", content);
            str = str.Replace("$MY_NAME$", myName);

            return str;
        }

        public static  string arrayToString(string[] strArray)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < strArray.Length; i++)
            {

                if (i > 0)
                {
                    //分割符可根据需要自行修改
                    str.Append(",");
                }
                str.Append(strArray[i]);
            }
            return str.ToString();
        }

        public static string ToUnixTime(DateTime datetime)
        {
            long time = datetime.ToUniversalTime().Ticks;

            return time.ToString();
        }
        public static string ToMd5(string input)
        {
            if (input == null)
            {
                input = string.Empty;
            }

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        #region 获取由SHA1加密的字符串
        public static string EncryptToSHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

        #region 获取由MD5加密的字符串
        public static string EncryptToMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            (md5 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

        public static string getRandomNumber(int begin,int end)
        {


            Random ran = new Random();
            int RandKey = ran.Next(begin, end);

            return RandKey.ToString();



        }

        private static char[] constant =
        {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        /// <summary> 
        /// 汉字转化为拼音
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>全拼</returns> 
        public static string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }



        /// <summary> 
        /// 汉字转化为拼音首字母
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>首字母</returns> 
        public static string GetFirstPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        public static string FirstToUpper(string input)
        {
            string lowstring = input.ToLower();

            string a = lowstring.Substring(0, 1).ToUpper();

            return a + lowstring.Remove(0, 1);


        }

        public static char GetFirstChar(string str)
        {
            char[] charArray = str.ToCharArray();

            char firserChar = charArray[0];

            return firserChar;

        }


        public static char ToUpper(char input)
        {

            return char.ToUpper(input);
        }

        public static char ToLower(char input)
        {

            return char.ToLower(input);
        }

        public static List<String> GetMontList()
        {
            List<String> MonthList = new List<String>();
            MonthList.Add("一月");
            MonthList.Add("二月");
            MonthList.Add("三月");
            MonthList.Add("四月");
            MonthList.Add("五月");
            MonthList.Add("六月");
            MonthList.Add("七月");
            MonthList.Add("八月");
            MonthList.Add("九月");
            MonthList.Add("十月");
            MonthList.Add("十一月");
            MonthList.Add("十二月");
            return MonthList;
        }

        public static List<Char> GetCharList()
        {
            List<Char> charList = new List<char>();
            charList.Add('A');
            charList.Add('B');
            charList.Add('C');
            charList.Add('D');
            charList.Add('E');
            charList.Add('F');
            charList.Add('G');
            charList.Add('H');
            charList.Add('I');
            charList.Add('J');
            charList.Add('K');
            charList.Add('L');
            charList.Add('M');
            charList.Add('N');
            charList.Add('O');
            charList.Add('P');
            charList.Add('Q');
            charList.Add('R');
            charList.Add('S');
            charList.Add('T');
            charList.Add('U');
            charList.Add('V');
            charList.Add('W');
            charList.Add('X');
            charList.Add('Y');
            charList.Add('Z');
            return charList;
        }
        public static bool Verify(string input)
        {
            var result = false;
            if (Regex.IsMatch(input, @"\d{17}(\d|x|X)"))
            {
                input = input.ToLower();
                int[] weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };    //十七位数字本体码权重
                char[] validate = { '1', '0', 'x', '9', '8', '7', '6', '5', '4', '3', '2' };
                int sum = 0;
                int mode = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum = sum + ((int)(input[i] - '0')) * weight[i];
                }
                mode = sum % 11;
                result = (validate[mode] == input[17]);
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static string GetSexByVerify(string identityCard)
        {
            string sexNum = "";
            string Sex = "";
            //处理18位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 18)
            {
               // birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                sexNum = identityCard.Substring(14, 3);
            }
            //处理15位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 15)
            {
              //  birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                sexNum = identityCard.Substring(12, 3);
            }
            if (int.Parse(sexNum) % 2 == 0)
            {
                Sex = "女";
            }
            else
            {
                Sex = "男";
            }
            return Sex;
        
        }
        public static string GetBirthdyByVerify(string identityCard)
        {
           
            string Birthday = "";
            //处理18位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 18)
            {
                Birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                
            }
            //处理15位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 15)
            {
                Birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
               
            }

            return Birthday;

        }
        

    }
}
