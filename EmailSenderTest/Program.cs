﻿using CD4.ReportDispatch.Mail;
using CD4.ReportDispatch.SDK.Models;
using CD4.ReportDispatch.SDK.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmailSenderTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dispatch = new Dispatch();
            var mailModel = new EmailModel()
            {
                Subject = $"CD4 LIMS: COVID-19 PCR Test Results {DateTime.Today:dd.MM.yyyy}",
                ToAddress = "ibrahim.hucyn@live.com",
                ToDisplayName = "Ibrahim",
                Template = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "medlab_reportdispatch_template.html"))
        };

            await dispatch.ExecuteAsync(new PapercutSmtpService(), await (new AttachmentService()).GetAttachmentsAsync(), mailModel);
            //await d.ExecuteAsync(new OutlookSmptService(), await (new AttachmentService()).GetAttachmentsAsync(), mailModel);

            //uncomment to send via gmail. have to have the API key in GmailSmptService class
            //await d.ExecuteAsync(new GmailSmptService(), await (new AttachmentService()).GetAttachmentsAsync(), mailModel);
        }
    }
}
