using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace coding.API.Helpers
{
    [ExcludeFromCodeCoverage]
    public class MailSender
    {
        public readonly IEmailConfiguration configuration;
        private bool EnableSslStatus;

        public MailSender(IEmailConfiguration configuration)
        {
            this.configuration = configuration;
            if (configuration is EmailConfigurationProd)
            {
                EnableSslStatus = true;

            }
            else
            {

                EnableSslStatus = false;
            }
        }
        public async Task SendEmailAsync(IMailTemplate template)
        {
            var fromAddress = new MailAddress(configuration.FromAddress, configuration.FromName);
            var toAddress = new MailAddress(template.Receiver, template.Receiver);
            string fromPassword = configuration.FromPassword;
            string subject = template.Subject;
            string body = template.Body;

            var smtp = new SmtpClient
            {
                Host = configuration.SmtpHost,
                Timeout = 1000,
                Port = configuration.SmtpPort,
                EnableSsl = EnableSslStatus,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(configuration.FromAddress, configuration.FromPassword),
            };
            using (var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                if (template.Attachments != null)
                {
                    template.Attachments.ForEach(attachment =>
                    {
                        mailMessage.Attachments.Add(attachment);
                    });
                }

                await smtp.SendMailAsync(mailMessage);

            }
        }
    }
}