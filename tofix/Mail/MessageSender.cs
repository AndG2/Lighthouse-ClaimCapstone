using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace tofix.Mail
{
    public static class MessageSender
    {
        private static readonly string _smtpHost = 
            ConfigurationManager.AppSettings["SmtpHost"];
        private static readonly int _smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        private static readonly string _smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
        private static readonly string _smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
        private static readonly string _fromAddress = ConfigurationManager.AppSettings["FromAddress"];

        public static void SendEmail(string toAddress, string subject, string messageBody)
        {
            using(var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUsername,_smtpPassword);

                using (var msg = new MailMessage())
                {
                    msg.From = new MailAddress(_fromAddress);
                    msg.To.Add(toAddress);
                    msg.Subject = subject;
                    msg.Body = messageBody;

                    client.Send(msg);
                }
            }
        }

    }
}