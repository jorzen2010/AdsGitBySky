using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Common;

namespace AdsWeb.WechatServices
{
    public class WechatMaterialServices
    {

        public static string GetMaterialList(string access_token, string postdata)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, postdata);

            return result;

        }






        /// <summary>
        /// 新增临时素材/上传多媒体文件
        /// http://mp.weixin.qq.com/wiki/5/963fc70b80dc75483a271298a76a8d59.html
        /// 1.上传的媒体文件限制：
        ///图片（image) : 1MB，支持JPG格式
        ///语音（voice）：1MB，播放长度不超过60s，支持MP4格式
        ///视频（video）：10MB，支持MP4格式
        ///缩略图（thumb)：64KB，支持JPG格式
        ///2.媒体文件在后台保存时间为3天，即3天后media_id失效
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
        /// <param name="fileName">文件名</param>
        /// <param name="inputStream">文件输入流</param>
        /// <returns>media_id</returns>
        public static string UploadTempMedia(string access_token, string type, string fileName, Stream inputStream)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", access_token, type.ToString());
            var result = CurlFileTools.HttpRequestPost(url, "media", fileName, inputStream);
            inputStream.Close();
            inputStream.Dispose();
            return result;
        }

        /// <summary>
        /// 获取临时素材/下载多媒体文件
        /// http://mp.weixin.qq.com/wiki/11/07b6b76a6b6e8848e855a435d5e34a5f.html
        /// 公众号可以使用本接口获取临时素材（即下载临时的多媒体文件）。请注意，视频文件不支持https下载，调用该接口需http协议。
        /// 本接口即为原“下载多媒体文件”接口。
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="access_token"></param>
        /// <param name="media_id"></param>
        public static void DownloadTempMedia(string savePath, string access_token, string media_id)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", access_token, media_id);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            CurlFileTools.Download(url, fs);
            fs.Close();
            fs.Dispose();
        }

        /// 上传永久素材
        /// <param name="savePath"></param>
        /// <param name="access_token"></param>
        /// <param name="media_id"></param>
        public static string UploadForeverMedia(string access_token, string type, string fileName, Stream inputStream)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}", access_token, type.ToString());
            var result = CurlFileTools.HttpRequestPost(url, "media", fileName, inputStream);
            inputStream.Close();
            inputStream.Dispose();
            return result;
        }

        /// 下载永久素材
        public static void GetForeverMedia(string savePath, string access_token, string media_id)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", access_token, media_id);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            CurlFileTools.Download(url, fs);
            fs.Close();
            fs.Dispose();
        }

    }
}