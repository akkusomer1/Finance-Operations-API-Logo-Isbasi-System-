using ZeusApp.Application.DTOs.Mail;
using ZeusApp.Application.DTOs.Settings;
using ZeusApp.Application.Interfaces.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;


namespace ZeusApp.Infrastructure.Shared.Services;

public class SMTPMailService : IMailService
{
    public MailSettings _mailSettings { get; }
    public ILogger<SMTPMailService> _logger { get; }

    public SMTPMailService(IOptions<MailSettings> mailSettings, ILogger<SMTPMailService> logger)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task SendAsync(MailRequest request)
    {
        try
        {
            var email = new MimeMessage { Sender = MailboxAddress.Parse(request.From ?? _mailSettings.From) };
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;

            var builder = new BodyBuilder { HtmlBody = request.Body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }
}
