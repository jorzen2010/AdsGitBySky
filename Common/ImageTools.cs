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
    public class ImageTools
    {
        /// <summary>
        /// 图片叠加
        /// </summary>
        public static string composeImage(string oldpath,string avatarpath,string newpath)
        {

            string NewImagePath = string.Empty;
           // string path = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/oldbg.png");
            string path = System.Web.HttpContext.Current.Server.MapPath(oldpath);
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
          //  System.Drawing.Image imgWarter = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/avatar.png"));
            System.Drawing.Image imgWarter = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(avatarpath));

            imgWarter = CutEllipse(imgWarter, new Rectangle(0, 0, 150, 150), new Size(150, 150));
          //  System.Drawing.Image newImage = CutEllipse(imgWarter, new Rectangle(0, 0, 150, 150), new Size(150, 150));


            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgWarter, new Rectangle((imgSrc.Width - imgWarter.Width)/2,
                                                 imgSrc.Height/2,
                                                 imgWarter.Width,
                                                 imgWarter.Height),
                        0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);

            }

          //  NewImagePath = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/NewImage.png");
            NewImagePath = System.Web.HttpContext.Current.Server.MapPath(newpath);
            imgSrc.Save(NewImagePath, System.Drawing.Imaging.ImageFormat.Png);

            return NewImagePath;
        
        }


        private static  Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }




        /// <summary>
        /// 文字叠加生成图片
        /// </summary>
        ///使用时候采用、\r\n换行 string str = @"开始时间:2016-1-1" + "\r\n"
                                                             // + "结束时间:2017-1-1"+"\r\n"
                                                             //+ "沙尘天气等级:2"+"\r\n"
                                                             //+ "PM10日均浓度最大值:2ug/m3"+"\r\n"
                                                             // + "影响范围:济南，青岛";
        public static string composeText(string oldpath, string addText, string newpath)
        {

            string NewTextImage = string.Empty;

           // string path = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/oldbg.png");
            string path = System.Web.HttpContext.Current.Server.MapPath(oldpath);
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);

            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
                using (Font f = new Font("宋体", 62))
                {
                    using (Brush b = new SolidBrush(Color.White))
                    {
                        g.DrawString(addText, f, b, 285, 880);
                    }
                }
            }
           // NewTextImage = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/NewTextImage.png");
            NewTextImage = System.Web.HttpContext.Current.Server.MapPath(newpath);
            imgSrc.Save(NewTextImage, System.Drawing.Imaging.ImageFormat.Png);


            return NewTextImage;

        }

        public static string composeTextPoint(string oldpath, string addText, string newpath,int x,int y)
        {

            string NewTextImage = string.Empty;

            // string path = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/oldbg.png");
            string path = System.Web.HttpContext.Current.Server.MapPath(oldpath);
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);


            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
                using (Font f = new Font("宋体", 50))
                {
                    using (Brush b = new SolidBrush(Color.White))
                    {
                        g.DrawString(addText, f, b, x, y);
                    }
                }
            }
            // NewTextImage = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/NewTextImage.png");
            NewTextImage = System.Web.HttpContext.Current.Server.MapPath(newpath);
            imgSrc.Save(NewTextImage, System.Drawing.Imaging.ImageFormat.Png);


            return NewTextImage;

        }


        /// <summary>
        /// 把文字转换才Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        public static Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width + 1);
                int height = (int)(sizef.Height + 1);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;

        }

        public static string  SaveTextToImage(string textstr)
        {

       
            //得到Bitmap(传入Rectangle.Empty自动计算宽高)
            Bitmap bmp = TextToBitmap(textstr, new Font("Arial", 16), Rectangle.Empty, Color.Black, Color.Wheat);


            //保存到桌面save.jpg
           // NewTextImage = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/NewTextImage.png");
            string NewTextToImage = System.Web.HttpContext.Current.Server.MapPath(@"/Resource/Image/NewTextToImage.png");
            bmp.Save(NewTextToImage, System.Drawing.Imaging.ImageFormat.Png);

            return NewTextToImage;
            
        
        }
    }
}
