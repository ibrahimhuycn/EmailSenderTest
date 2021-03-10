using CD4.ReportDispatch.SDK.Models;
using CD4.ReportDispatch.SDK.Services;
using FluentEmail.Core;
using FluentEmail.Razor;
using System;
using System.Threading.Tasks;

namespace CD4.ReportDispatch.Mail
{
    public class Dispatch
    {
        public async Task ExecuteAsync(ISmtpService smtpService,
            AttachmentCollection attachments, EmailModel emailModel)
        {
            Email.DefaultSender = smtpService.GetSmtpSender();
            Email.DefaultRenderer = new RazorRenderer();

            var maxMailSize = 25;

            if ((attachments.AttachmentSize / (1024 * 1024)) < maxMailSize)
            {
                var email = await Email
                    .From(smtpService.SmtpSettings.FromAddress, "swatincadmin")
                    .To(emailModel.ToAddress, emailModel.ToDisplayName)
                    .Subject(emailModel.Subject)
                    .Attach(attachments.Attachments)
                    .UsingTemplate(emailModel.Template, emailModel.TemplateModel)
                    .SendAsync();
            }
            else
            {
                throw new Exception($"Cannot mail since the mail size exceeds maximum allowed {maxMailSize} MB");
            }

        }
    }

}
