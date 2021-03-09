using CD4.ReportDispatch.Mail;
using CD4.ReportDispatch.SDK.Services;
using System.Threading.Tasks;

namespace EmailSenderTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Dispatch d = new Dispatch();

            await d.ExecuteAsync(new PapercutSmtpService(), await (new AttachmentService()).GetAttachmentsAsync());

            //uncomment to send via gmail. have to have the API key in GmailSmptService class
            //await d.ExecuteAsync(new GmailSmptService(), await (new AttachmentService()).GetAttachmentsAsync());
        }
    }
}
