using Microsoft.Extensions.Options;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Models.Emails;
using MyPennyPincher_API.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MyPennyPincher_API.Services;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _smtp;
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtp = smtpOptions.Value;

        _smtpClient = new SmtpClient(_smtp.Host)
        {
            Port = _smtp.Port,
            Credentials = new NetworkCredential(_smtp.Username, _smtp.Password),
            EnableSsl = _smtp.EnableSsl,
        };
    }

    public void Send2FAEmail(MFAEmail email, string toEmail)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtp.Username),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        _smtpClient.Send(mailMessage);
    }

    public void SendPasswordChangeEmail()
    {
        throw new NotImplementedException();
    }

    public void SendVerificationEmail(VerificationEmail email, string toEmail)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtp.Username),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        _smtpClient.Send(mailMessage);
    }
}
