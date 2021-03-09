using CD4.ReportDispatch.SDK.Models;
using CD4.ReportDispatch.SDK.Services;
using FluentEmail.Core;
using FluentEmail.Razor;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CD4.ReportDispatch.Mail
{
    public class Dispatch
    {
        public async Task ExecuteAsync(ISmtpService smtpSettings, AttachmentModel attachments)
        {
            Email.DefaultSender = smtpSettings.GetSmtpSender();
            Email.DefaultRenderer = new RazorRenderer();

            var template = new StringBuilder();
            template.AppendLine("Dear @Model.FullName");

            if ((attachments.AttachmentSize / (1024 * 1024)) < 25)
            {
                var email = await Email
                    .From(smtpSettings.FromAddress)
                    .To("ibrahim.hucyn@live.com", "bob")
                    .Subject("hows it going bob")
                    .Attach(attachments.Attachments)
                    .UsingTemplate(template.ToString(), new {FullName = "Bob"})
                    .SendAsync();
            }
            else
            {
                Console.WriteLine("Attachment size exceeded 25MB");
            }

        }
    }
}
