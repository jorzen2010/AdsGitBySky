using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Common;

namespace AdsWeb.AliyunVideo

{
    public class AliyunVideoServices
    {  
        
        public static string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        public static string GetVideoPlayAuth()
        {
            string PlayAuth = "";
            return PlayAuth;
        }

      

        public static string GetSignature(string key, string StringToSign)
        {

            //string Sign = HttpUtility.UrlEncode(SkyEncrypt.HmacSha1(key, StringToSign));
            //return Sign;

            byte[] bytes = Encoding.Default.GetBytes(SkyEncrypt.HmacSha1(key, StringToSign));
         //   string Sign = HttpUtility.UrlEncode(Convert.ToBase64String(bytes));
            string Sign = AliyunVideoServices.UrlEncode(Convert.ToBase64String(bytes));
            
            return Sign;
        
        }

        public static string GetSignUrl(string key, string VideoId, string Timestamp, string Action, string SignatureNonce)
        {
            string url="";
            string StringToSign = AliyunVideoServices.GetStringToSign(VideoId, Timestamp, Action, SignatureNonce);
            string Signature = AliyunVideoServices.GetSignature(key, StringToSign);

            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Format", AliyunCommonParaConfig.Format);
            dics.Add("Version", AliyunCommonParaConfig.Version);
            dics.Add("AccessKeyId", AliyunCommonParaConfig.AccessKeyId);
            dics.Add("SignatureVersion", AliyunCommonParaConfig.SignatureVersion);
            dics.Add("SignatureMethod", AliyunCommonParaConfig.SignatureMethod);
            dics.Add("Timestamp", Timestamp);
            dics.Add("SignatureNonce", SignatureNonce);
            dics.Add("Action", Action);
            dics.Add("VideoId", VideoId);
            dics.Add("Signature", Signature);
            url = getParamSrc(dics);

            return url;
            

        }


        public static string GetStringToSign(string VideoId, string Timestamp, string Action, string SignatureNonce)
        {
           
            
            //字典排序，就是需要将那个值进行分割，分割后再重新组合，就行了。
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Format", AliyunCommonParaConfig.Format);
            dics.Add("Version", AliyunCommonParaConfig.Version);
            dics.Add("AccessKeyId", AliyunCommonParaConfig.AccessKeyId);
            dics.Add("SignatureVersion", AliyunCommonParaConfig.SignatureVersion);
            dics.Add("SignatureMethod", AliyunCommonParaConfig.SignatureMethod);
            dics.Add("Timestamp", Timestamp);
            dics.Add("SignatureNonce", SignatureNonce);
            dics.Add("Action", Action);
            dics.Add("VideoId", VideoId);

            string StringToSign = "GET" + "&" + AliyunVideoServices.UrlEncode("/") + "&" + AliyunVideoServices.UrlEncode(getParamSrc(dics));

            return StringToSign;
        }



        public static String getParamSrc(Dictionary<string, string> paramsMap)
        {
            var vDic = (from objDic in paramsMap orderby objDic.Key ascending select objDic);
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                string pkey =kv.Key;
                string pvalue =kv.Value;
                str.Append(pkey + "=" + pvalue + "&");
            }

            String result = str.ToString().Substring(0, str.ToString().Length - 1);
            return result;
        }

    }
}