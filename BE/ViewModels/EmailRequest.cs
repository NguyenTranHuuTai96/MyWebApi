using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class EmailRequest
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public List<MailAttachment>? AttachmentFile { get; set; }
    }
    public class MailAttachment
    {
        public string ContentType { get; set; }
        public byte[] Stream { get; set; }
    }
}
