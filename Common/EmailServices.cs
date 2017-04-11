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
        public static void SendEmail(EmailEntity mailEntity)
        {

            MailMessage mail = new MailMessage();

            mail.To.Add(mailEntity.ToMail);
            mail.From = new MailAddress(mailEntity.FromMail, mailEntity.DisplayName, System.Text.Encoding.GetEncoding("utf-8"));

            mail.Subject = mailEntity.SMTPClient;
            mail.Body = mailEntity.MailContent;
            mail.IsBodyHtml = true;
            try
            {
                SmtpClient smtpClient = new SmtpClient(mailEntity.SMTPClient);
                smtpClient.Credentials = new NetworkCredential(mailEntity.EmailAddress, mailEntity.EmailPassword);
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
        public string SMTPClient { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string ToMail { get; set; }
        public string FromMail { get; set; }
        public string DisplayName { get; set; }
        public string MailTitle { get; set; }
        public string MailContent { get; set; }
    
    }
}
