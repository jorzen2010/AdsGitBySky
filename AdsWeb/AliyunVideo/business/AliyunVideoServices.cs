using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace AdsWeb.AliyunVideo

{
    public class AliyunVideoServices
    {
        public static string GetVideoPlayAuth()
        {
            string PlayAuth = "";
            return PlayAuth;
        }

      
        public static string GetSign()
        {
            string Sign = "";
            return Sign;
        
        }


        public static string GetStringToSign()
        {
            //字典排序，就是需要将那个值进行分割，分割后再重新组合，就行了。
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Format", AliyunCommonParaConfig.Format);
            dics.Add("Version", AliyunCommonParaConfig.Version);
            dics.Add("AccessKeyId", AliyunCommonParaConfig.AccessKeyId);
            dics.Add("SignatureVersion", AliyunCommonParaConfig.SignatureVersion);
            dics.Add("SignatureMethod", AliyunCommonParaConfig.SignatureMethod);

           
            return  HttpUtility.UrlEncode(getParamSrc(dics));
        }

        public static String getParamSrc(Dictionary<string, string> paramsMap)
        {
            var vDic = (from objDic in paramsMap orderby objDic.Key ascending select objDic);
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                string pkey = kv.Key;
                string pvalue = kv.Value;
                str.Append(pkey + "=" + pvalue + "&");
            }

            String result = str.ToString().Substring(0, str.ToString().Length - 1);
            return result;
        }

    }
}