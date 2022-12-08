using Microsoft.AspNetCore.Identity.UI.Services;

namespace CRM.Services.Identity.MessageSenders
{
    public class EmailMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
