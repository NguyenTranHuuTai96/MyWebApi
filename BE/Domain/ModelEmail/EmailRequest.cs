using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelEmail
{
    internal class EmailRequest
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string[] AttachmentFilePaths { get; set; } = Array.Empty<string>();
    }
}
