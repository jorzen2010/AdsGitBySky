using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    public class EmailServices
    {

        //发送邮件服务
        public static void SendEmail(EmailServer emailServer,EmailEntity mailEntity)
        {

            MailMessage mail = new MailMessage();

            mail.To.Add(mailEntity.ToMail);
            mail.From = new MailAddress(mailEntity.FromMail, mailEntity.DisplayName, System.Text.Encoding.GetEncoding("utf-8"));

            mail.Subject = mailEntity.MailTitle;
            mail.Body = mailEntity.MailContent;
            mail.IsBodyHtml = true;
            try
            {
                SmtpClient smtpClient = new SmtpClient(emailServer.SMTPClient);
                smtpClient.Credentials = new NetworkCredential(emailServer.EmailAddress, emailServer.EmailPassword);
                smtpClient.Send(mail);

            }
            catch (Exception exception)
            {

                throw (exception);
            }
        }
    }

    public class EmailEntity
    {
        public string ToMail { get; set; }
        public string FromMail { get; set; }
        public string DisplayName { get; set; }
        public string MailTitle { get; set; }
        public string MailContent { get; set; }
    
    }

    public class EmailServer
    {
        public string SMTPClient { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
    
    }

    public class EmailConfig
    {
        //=======【可以使用配置信息，也可以使用数据库信息】=====================================

        public const string SMTPClient = "smtp.126.com";
        public const string ServerPort = "25";
        public const string EmailAddress = "psycoder@126.com";
        public const string EmailUser = "psycoder";
        public const string EmailPassword = "jorzen2008";


    }
}
