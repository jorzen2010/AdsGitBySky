using System;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace Common
{
    public class FileServices
    {
        //使用说明：根目录最前方应该没有分隔符
        // string rootpath = "Resource"; 一层
        // string rootpath = "Resource/Upload";两层
        public static string Uploadfiles(string rootpath,string folder, HttpPostedFileBase file)
        {
            Random random = new Random();
            //提供平台特定的替换字符，该替换字符用于在反映分层文件系统组织的路径字符串中分隔目录级别
            var sep = Path.AltDirectorySeparatorChar.ToString();
            //指定为根目录
            var root = "~" + sep + rootpath + sep;
            //拼接成路径
            var path = root + folder + sep;
            //找到这个路径
            var phicyPath = HostingEnvironment.MapPath(path);
            //判断是否存在，不存在创建一个
            if (!Directory.Exists(phicyPath))
            {
                Directory.CreateDirectory(phicyPath);
            }
            //获取文件类型
            string extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //拼接文件名
            string filename = System.DateTime.Now.ToUniversalTime().Ticks.ToString() + random.Next(1000, 9999).ToString() + extension;

            //保存文件
            file.SaveAs(phicyPath + filename);

            string fileuploadpath = path + filename;

            return fileuploadpath;

        }

        public static string UploadBase64(string rootpath, string folder, string base64)
        {
            Random random = new Random();
            //提供平台特定的替换字符，该替换字符用于在反映分层文件系统组织的路径字符串中分隔目录级别
            var sep = Path.AltDirectorySeparatorChar.ToString();
            //指定为根目录
            var root = "~" + sep + rootpath + sep;
            //拼接成路径
            var path = root + folder + sep;
            //找到这个路径
            var phicyPath = HostingEnvironment.MapPath(path);
            //判断是否存在，不存在创建一个
            if (!Directory.Exists(phicyPath))
            {
                Directory.CreateDirectory(phicyPath);
            }

            //拼接文件名
            string filename = System.DateTime.Now.ToUniversalTime().Ticks.ToString() + random.Next(1000, 9999).ToString() +".jpg";


            string filePath = rootpath + sep + folder + sep + filename;

            FileTools.Base64ToImage(base64, filePath);



            return filePath;

        }
    }
}
