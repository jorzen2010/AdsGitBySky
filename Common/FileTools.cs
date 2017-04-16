using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web;

namespace Common
{
    public class FileTools
    {
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// 将 byte[] 转成 Stream

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static FileStream StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
            return fs;
        }


        public static Bitmap Base64StringToImage(string inputStr,string filePath)
        {
            byte[] arr = Convert.FromBase64String(inputStr);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);

         //   bmp.Save(System.Web.HttpContext.Current.Server.MapPath(filePath), System.Drawing.Imaging.ImageFormat.Png);
            bmp.Save(System.Web.HttpContext.Current.Server.MapPath(filePath), System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Close();
            return bmp;
   
        }


        public static Bitmap Base64ToImage(string inputStr, string filePath)
        {
            //这段代码的意思是去除base64前面的前缀//data:image/jpg;base64,   
            string byteStr = inputStr.ToString();//data:image/jpg;base64,         
            int delLength = byteStr.IndexOf(',') + 1;
            string img64 = byteStr.Substring(delLength, byteStr.Length - delLength);

            byte[] arr = Convert.FromBase64String(img64);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);

            //   bmp.Save(System.Web.HttpContext.Current.Server.MapPath(filePath), System.Drawing.Imaging.ImageFormat.Png);
            bmp.Save(System.Web.HttpContext.Current.Server.MapPath(filePath), System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Close();
            return bmp;

        }


        //追加内容
        public static void WriteTxt(string filePath, string content)
        {

           // StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "\\myText.txt");
            StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(filePath));
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();

        }

        public static void DeleteFile(string filePath)
        {


           // string delFile = HttpContext.Current.Server.MapPath(".") + "\\myTextCopy.txt";
            string delFile = HttpContext.Current.Server.MapPath(filePath) ;
            if (!string.IsNullOrEmpty(delFile))
            { 
            File.Delete(delFile);
            }
            
        }


        public static void CopyFile(string filePath,string newFilePath)
        {
            string OrignFile, NewFile;
            //OrignFile = Server.MapPath(".") + "\\myText.txt";
            //NewFile = Server.MapPath(".") + "\\myTextCopy.txt";

            OrignFile = HttpContext.Current.Server.MapPath(filePath);
            NewFile = HttpContext.Current.Server.MapPath(newFilePath);
            File.Copy(OrignFile, NewFile, true);
        }

        //// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input"></param>
        private void WriteLogFile(string filePath,string input)
        {
            /**/
            ///指定日志文件的目录
           // string fname = Server.MapPath("upedFile") + "\\logfile.txt";
            string fname = HttpContext.Current.Server.MapPath(filePath);
            /**/
            ///定义文件信息对象
            FileInfo finfo = new FileInfo(fname);

            /**/
            ///判断文件是否存在以及是否大于2K
            if (finfo.Exists && finfo.Length > 2048)
            {
                /**/
                ///删除该文件
                finfo.Delete();
            }
            /**/
            ///创建只写文件流
            using (FileStream fs = finfo.OpenWrite())
            {
                /**/
                ///根据上面创建的文件流创建写数据流
                StreamWriter w = new StreamWriter(fs);

                /**/
                ///设置写数据流的起始位置为文件流的末尾
                w.BaseStream.Seek(0, SeekOrigin.End);

                /**/
                ///写入“Log Entry : ”
                w.Write("\nLog Entry : ");

                /**/
                ///写入当前系统时间并换行
                w.Write("{0} {1} \r\n", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());

                /**/
                ///写入日志内容并换行
                w.Write(input + "\n");

                /**/
                ///写入------------------------------------“并换行
                w.Write("------------------------------------\n");

                /**/
                ///清空缓冲区内容，并把缓冲区内容写入基础流
                w.Flush();

                /**/
                ///关闭写数据流
                w.Close();
            }
        }

        // ======================================================
        // 实现一个静态方法将指定文件夹下面的所有内容copy到目标文件夹下面
        // 如果目标文件夹为只读属性就会报错。
        // April 18April2005 In STU
        // ======================================================
        public static string CopyDir(string srcPath, string aimPath)
        {
            string msg = "";
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath)) Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

                msg = "成功了";
            }
            catch (Exception e)
            {
                msg=e.ToString();
            }
            return msg;
        }

        // ======================================================
        // 实现一个静态方法将指定文件夹下面的所有内容Detele
        // 测试的时候要小心操作，删除之后无法恢复。
        // April 18April2005 In STU
        // ======================================================
        public static string DeleteDir(string aimPath)
        {
            string msg = "";
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(aimPath);
                string[] fileList = Directory.GetFileSystemEntries(aimPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        DeleteDir(aimPath + Path.GetFileName(file));
                    }
                    // 否则直接Delete文件
                    else
                    {
                        File.Delete(aimPath + Path.GetFileName(file));
                    }
                }
                //删除文件夹
                System.IO.Directory.Delete(aimPath, true);
                msg = "成功了";
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            return msg;
        }

        /**/
        /// <summary>
        /// 拷贝文件夹(包括子文件夹)到指定文件夹下,源文件夹和目标文件夹均需绝对路径. 格式: CopyFolder(源文件夹,目标文件夹);
        /// </summary>
        /// <param name="strFromPath"></param>
        /// <param name="strToPath"></param>

        //--------------------------------------------------
        //作者:明天去要饭 QQ:305725744
        //---------------------------------------------------

        public static void CopyFolder(string strFromPath, string strToPath)
        {
            //如果源文件夹不存在，则创建
            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }

            //取得要拷贝的文件夹名
            string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") + 1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);

            //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
            if (!Directory.Exists(strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(strToPath + "\\" + strFolderName);
            }
            //创建数组保存源文件夹下的文件名
            string[] strFiles = Directory.GetFiles(strFromPath);

            //循环拷贝文件
            for (int i = 0; i < strFiles.Length; i++)
            {
                //取得拷贝的文件名，只取文件名，地址截掉。
                string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
                //开始拷贝文件,true表示覆盖同名文件
                File.Copy(strFiles[i], strToPath + "\\" + strFolderName + "\\" + strFileName, true);
            }

            //创建DirectoryInfo实例
            DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
            //取得源文件夹下的所有子文件夹名称
            DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
            for (int j = 0; j < ZiPath.Length; j++)
            {
                //获取所有子文件夹名
                string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();
                //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
                CopyFolder(strZiPath, strToPath + "\\" + strFolderName);
            }
        }




    }
}
