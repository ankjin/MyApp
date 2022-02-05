using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebAppShared.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //private readonly IWebHostEnvironment _hostingEnv;
        //public EmailSender(IWebHostEnvironment hostingEnv)
        //{
        //    _hostingEnv = hostingEnv;
        //}

        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailHtmlAsync(string email, string name, string subject, string link, string htmlTemplate, string optparam1 = "", string optparam2 = "")
        {
            //calling for creating the email body with html template   

            string body = this.createEmailBody(name, link, htmlTemplate, optparam1, optparam2);

            SmtpMail(email, subject, body);

            return Task.CompletedTask;
        }

        private string createEmailBody(string name, string link, string htmlTemplate, string optparam1 = "", string optparam2 = "")
        {
            string webRootPath = ""; //_hostingEnv.WebRootPath;

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(webRootPath + "/emailtemplate/" + htmlTemplate + ".html"))
            {
                body = reader.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(name))
            {
                body = body.Replace("{Name}", name); //replacing the required things
            }
            if (!string.IsNullOrEmpty(link))
            {
                body = body.Replace("{Link}", link);
            }
            if (!string.IsNullOrEmpty(optparam1))
            {
                body = body.Replace("{ORNo}", optparam1);
            }
            if (!string.IsNullOrEmpty(optparam2))
            {
                body = body.Replace("{Address}", optparam2);
            }
            return body;

        }

        public Task SmtpMail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage
            (
                _config["FromEmail"].ToString(), email, subject, body
            );
            mail.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    // AAB
                    smtp.Host = "mail.aabqatar.com";
                    smtp.Port = 25;
                    smtp.Credentials = new NetworkCredential("customerservice@aabqatar.com", "cs1321!");

                    //// Smart
                    //smtp.Host = "mail.ankpos.com";
                    //smtp.Port = 25;
                    //smtp.Credentials = new NetworkCredential("postmaster@ankpos.com", "postMASTER@09");

                    smtp.Send(mail);
                    mail = null;
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                return Task.CompletedTask;
            }
        }




        //Used for normal email(none html tamplate)
        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mail = new MailMessage
                (
                    _config["FromEmail"].ToString(), email, subject, message
                );
            mail.IsBodyHtml = false;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    // AAB
                    smtp.Host = "mail.aabqatar.com";
                    smtp.Port = int.Parse(_config["Smtp:prt"].ToString());
                    smtp.Credentials = new NetworkCredential(_config["Smtp:usr"].ToString(), _config["Smtp:pwd"].ToString());

                    smtp.Send(mail);
                    mail = null;
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                return Task.CompletedTask;
            }
        }


    }
}
