using Domain.ModelEmail;
using IServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Services
{
    public class EmailExtend : IEmailExtend
    {
        EmailConfig _emailConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailExtend(IOptions<EmailConfig> emailConfig, IWebHostEnvironment webHostEnvironment )
        {
            _emailConfig = emailConfig.Value;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port); // Create instance  
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password); // Info 
                smtpClient.UseDefaultCredentials = false; 
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailConfig.DefaultSender??string.Empty);
                mailMessage.To.Add(emailRequest.To ?? string.Empty);
                mailMessage.IsBodyHtml = true; // read template html
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Content;

                if (emailRequest.AttachmentFile?.Count > 0)
                {
                    foreach (var attach in emailRequest.AttachmentFile)
                    {
                        Attachment attachment = new Attachment(new MemoryStream(attach.Stream),attach.ContentType);

                        mailMessage.Attachments.Add(attachment);
                    }
                }

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);

                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
        }
        public async Task<string> GetTemplate(string templateName)
        {
            string templateEmail = Path.Combine(_webHostEnvironment.ContentRootPath, templateName);

            string content = await File.ReadAllTextAsync(templateEmail);

            return content;
        }
    }
}
