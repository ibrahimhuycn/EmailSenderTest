using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CD4.ReportDispatch.SDK.Services
{
    public class PapercutSmtpService : ISmtpService
    {
        public PapercutSmtpService()
        {
            HostUrl = "localhost";
            Port = 25;
            EnableSsl = false;
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
                Port = Port,
            });
        }
    }
}
