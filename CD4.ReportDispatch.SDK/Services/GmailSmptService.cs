using FluentEmail.Smtp;

namespace CD4.ReportDispatch.SDK.Services
{
    public class GmailSmptService : ISmtpService
    {
        public GmailSmptService()
        {
            HostUrl = "smtp.gmail.com";
            Port = 587;
            EnableSsl = true;
            ApiKey = "";
            FromAddress = "ibrahim.hucyn@gmail.com";
        }
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public bool EnableSsl { get; set; }
        public string HostUrl { get; set; }
        public int Port { get; set; }

        public SmtpSender GetSmtpSender()
        {
            return new SmtpSender(() => new System.Net.Mail.SmtpClient(HostUrl)
            {
                EnableSsl = EnableSsl,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(FromAddress, ApiKey),
                Port = Port,
            });
        }
    }
}
