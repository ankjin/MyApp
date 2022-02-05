using System.Threading.Tasks;

namespace WebAppShared.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailHtmlAsync(string email, string name, string subject, string link, string htmlTemplate, string optparam1 = "", string optparam2 = "");
    }
}
