using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Common
{
    public enum LogType
    {
        Overall,
    }

    public class LogHelper
    {
        public static string LogPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\log";
            }
        }

        public enum LogLevel
        {
            Info,
            Error
        }

        

        public static void Info(string message, LogType logType = LogType.Overall)
        {
            if (string.IsNullOrEmpty(message))
                return;
            var path = string.Format(@"\{0}\", logType.ToString());
            WriteLog(path, "", message);
        }
        public static void Error(string message, LogType logType = LogType.Overall)
        {
            if (string.IsNullOrEmpty(message))
                return;
            var path = string.Format(@"\{0}\", logType.ToString());
            WriteLog(path, "Error ", message);
        }

        public static void Error(Exception e, LogType logType = LogType.Overall)
        {
            if (e == null)
                return;
            var path = string.Format(@"\{0}\", logType.ToString());
            WriteLog(path, "Error ", e.Message);
        }

        private static void WriteLog(string path, string prefix, string message)
        {
            path = LogPath + path;
            var fileName = string.Format("{0}{1}.log", prefix, DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (FileStream fs = new FileStream(path + fileName, FileMode.Append, FileAccess.Write,
                                                  FileShare.Write, 1024, FileOptions.Asynchronous))
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\r\n");
                IAsyncResult writeResult = fs.BeginWrite(buffer, 0, buffer.Length,
                    (asyncResult) =>
                    {
                        var fStream = (FileStream)asyncResult.AsyncState;
                        fStream.EndWrite(asyncResult);
                    },

                    fs);
                fs.Close();
            }
        }

        public static string ReadLog(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            String line;
            string x = "";
            while ((line = sr.ReadLine()) != null)
            {
                x += line.ToString() + "<br/>";
                // Console.WriteLine(line.ToString());
            }
            sr.Close();

            return x;
        
        }
    }
}