using FluentEmail.Smtp;

namespace CD4.ReportDispatch.SDK.Services
{
    public interface ISmtpService
    {
        bool EnableSsl { get; set; }
        string HostUrl { get; set; }
        int Port { get; set; }
        string FromAddress { get; set; }
        string ApiKey { get; set; }

        SmtpSender GetSmtpSender();
    }
}