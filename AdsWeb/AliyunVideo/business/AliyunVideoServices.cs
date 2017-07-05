using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Common;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json;


namespace AdsWeb.AliyunVideo

{
    public class AliyunVideoServices
    {  
        
       
        public static string GetCanonicalizedQueryString(string VideoId, string Timestamp, string Action, string SignatureNonce)
        {
            //构造没有签名的公共参数
            CommonParam Param = new CommonParam();
            Param.AccessKeyId = AliyunCommonParaConfig.AccessKeyId;
            Param.Format = AliyunCommonParaConfig.Format;
            Param.SignatureMethod = AliyunCommonParaConfig.SignatureMethod;           
            Param.SignatureVersion = AliyunCommonParaConfig.SignatureVersion;
            Param.Version = AliyunCommonParaConfig.Version;
            Param.Timestamp = Timestamp;
            Param.SignatureNonce = SignatureNonce;
            
            //字典排序，就是需要将那个值进行分割，分割后再重新组合，就行了。
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Format", Param.Format);
            dics.Add("Version", Param.Version);
            dics.Add("AccessKeyId", Param.AccessKeyId);
            dics.Add("SignatureVersion", Param.SignatureVersion);
            dics.Add("SignatureMethod", Param.SignatureMethod);
            dics.Add("Timestamp", Param.Timestamp);
            dics.Add("SignatureNonce", Param.SignatureNonce);
            dics.Add("Action", Action);
            dics.Add("VideoId", VideoId);
            //第一步得到规范化字符串
            string CanonicalizedQueryString = getParamSrc(dics);

            return CanonicalizedQueryString;
        }

        public static string GetStringToSign(string CanonicalizedQueryString)
        {


           
            //第二步用于签名的字符串。

           string StringToSign = "GET" + "&" + CommonTools.UrlEncodeToUpper("/") + "&" + CommonTools.UrlEncodeToUpper(CanonicalizedQueryString);

            return StringToSign;
        }


        public static string GetSignature(string key, string StringToSign)
        {
            //第三步得到签名HMAC值

            var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(StringToSign));
            //第四步转化为base64编码字符串的Signature
            var Signature = Convert.ToBase64String(hashBytes).ToString();
            return Signature;

        }


        public static string GetSignUrl(string VideoId, string Timestamp, string Action, string SignatureNonce, string Signature)
        {
            //第五步得到签名的URL
           

            string url = "";

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

        public static string  VideoInfoJsonStr(string url)
        {

            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            HttpWebResponse res = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);
            Stream stream = res.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            string VideoInfoJsonStr = result;
            return VideoInfoJsonStr;
        
        }

        public static VideoInfo videoInfo(string VideoInfoJson)
        {
             VideoInfo  VideoInfo = new  VideoInfo();
             VideoInfo = JsonConvert.DeserializeObject<VideoInfo>(VideoInfoJson);
             return VideoInfo;

 
        }

        public static VideoInfo GetVideoInfo(string ApiUrl,string VideoId, string Timestamp, string Action, string SignatureNonce)
        {
            // 第一步构造规范化请求字符串。
            string CanonicalizedQueryString = AliyunVideoServices.GetCanonicalizedQueryString(VideoId, Timestamp, Action, SignatureNonce);
            //第二步用于签名的字符串。
            string StringToSign = AliyunVideoServices.GetStringToSign(CanonicalizedQueryString);
            //第三步得到签名HMAC值
            string key = AliyunCommonParaConfig.AccessKeySecret + "&";
            // string SignHMAC = AliyunVideoServices.GetSignHMAC(key,StringToSign);
            //第四步转化为base64编码字符串的Signature
            string Signature = AliyunVideoServices.GetSignature(key, StringToSign);
            //第五步得到签名的URL
            string SignUrl = "http://" + ApiUrl + "?" + AliyunVideoServices.GetSignUrl(VideoId, Timestamp, Action, SignatureNonce, Signature);


            //通过URL获取videoinfo信息
            string videoInfoStr = AliyunVideoServices.VideoInfoJsonStr(SignUrl);
            //通过URL获取videoinfo信息

            VideoInfo videoInfo = AliyunVideoServices.videoInfo(videoInfoStr);

            return videoInfo;


        }




        public static String getParamSrc(Dictionary<string, string> paramsMap)
        {
            var vDic = (from objDic in paramsMap orderby objDic.Key ascending select objDic);
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
               string pkey = CommonTools.UrlEncodeToUpper(kv.Key).Replace("+", "20%").Replace("*", "%2A").Replace("%7E", "~");
               string pvalue = CommonTools.UrlEncodeToUpper(kv.Value).Replace("+", "20%").Replace("*", "%2A").Replace("%7E", "~");       
               str.Append(pkey + "=" + pvalue + "&");
            }

            String result = str.ToString().Substring(0, str.ToString().Length - 1);
            return result;
        }

    }
}