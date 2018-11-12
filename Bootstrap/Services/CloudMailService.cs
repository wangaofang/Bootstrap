using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Bootstrap.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "noreply@alibaba.com";

        private readonly ILogger<CloudMailService> _logger;

         public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}