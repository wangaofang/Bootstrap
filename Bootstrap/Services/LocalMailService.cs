using System.Diagnostics;

namespace Bootstrap.Services
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }
    public class LocalMailService:IMailService
    {
         private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}