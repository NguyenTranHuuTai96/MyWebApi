using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace IServices
{
    public interface IEmailExtend
    {
        Task<string> GetTemplate(string templateName);
        Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}
