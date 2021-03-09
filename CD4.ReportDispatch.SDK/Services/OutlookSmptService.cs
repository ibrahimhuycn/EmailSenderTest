using FluentEmail.Smtp;

namespace CD4.ReportDispatch.SDK.Services
{
    public class OutlookSmptService : ISmtpService
    {
        public OutlookSmptService()
        {
            HostUrl = "";
            Port = 465;
            EnableSsl = true;
        }
        public bool EnableSsl { get; set; }
        public string HostUrl { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string ApiKey { get; set; }

        public SmtpSender GetSmtpSender()
        {
            throw new System.NotImplementedException();
        }
    }
}
