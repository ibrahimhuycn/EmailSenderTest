using CD4.ReportDispatch.SDK.Models;
using FluentEmail.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CD4.ReportDispatch.SDK.Services
{
    public class AttachmentService
    {
        public AttachmentService()
        {
            Initialize();
        }

        private void Initialize()
        {
            //get the information from somewhere
            Directory = @"C:\Export";
            Extension = "*.pdf";
            ContentType = "application/pdf";
        }

        public string ContentType { get; set; }
        public string Extension { get; set; }
        public string Directory { get; set; }

        public async Task<AttachmentModel> GetAttachmentsAsync()
        {
            var directoryInfor = new DirectoryInfo(Directory);
            var fileInfos = await Task.Run(() => directoryInfor.GetFiles(Extension)); //Getting files

            List<Attachment> attachments = new List<Attachment>();

            var attachmentSize = 0L;
            foreach (var file in fileInfos)
            {
                attachmentSize += file.Length; 
                attachments.Add(new Attachment()
                {
                    ContentId = Guid.NewGuid().ToString(),
                    ContentType = ContentType, 
                    Filename = file.Name,
                    IsInline = false,
                    Data = new FileStream(file.FullName, FileMode.Open)
                });
            }

            return new AttachmentModel() { Attachments = attachments, AttachmentSize = attachmentSize };
        }
    }
}
