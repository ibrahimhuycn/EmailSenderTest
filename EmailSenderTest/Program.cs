using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EmailSenderTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Port= 25
            });

            Email.DefaultSender = sender;




            DirectoryInfo d = new DirectoryInfo(@"C:\Export");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pdf"); //Getting pdf files

            
            List<Attachment> attachments = new List<Attachment>();

            // I guess you are going to make sure that the size of attachments does not 
            // exceed 25 MB or something. The limit.

            //may be zip them all up and attach
            var attachmentSize = 0L;
            foreach (var file in Files)
            {
                attachmentSize += file.Length;
                attachments.Add(new Attachment()
                {
                    ContentId = Guid.NewGuid().ToString(),
                    ContentType = "application/pdf", // if text files use somthing like : text/plain 
                    Filename = file.Name,
                    IsInline = false,
                    Data = new FileStream(file.FullName, FileMode.Open) // I wonder when the file stream gets closed!
                });
            }

            //assuming that the limit is 25MB. Does't really matter unless you try to send a real one.
            var attachmentSizeInMB = attachmentSize / (1024 * 1024);
            Console.WriteLine($"Attachment size is {attachmentSizeInMB} MB.");
            if ( attachmentSizeInMB < 25)
            {
                var email = await Email
                    .From("john@email.com")
                    .To("bob@email.com", "bob")
                    .Subject("hows it going bob")
                    .Attach(attachments)
                    .Body("yo bob, long time no see!")
                    .SendAsync();
            }
            else
            {
                Console.WriteLine("Attachment size exceeded 25MB");
            }



            attachments = null;
        }
    }
}
