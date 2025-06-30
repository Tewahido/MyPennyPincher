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

    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtp = smtpOptions.Value;
    }

    public void Send2FAEmail(MFAEmail email)
    {
        var smtpClient = new SmtpClient(_smtp.Host)
        {
            Port = 587,
            Credentials = new NetworkCredential(_smtp.Username,_smtp.Password),
            EnableSsl = _smtp.EnableSsl,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtp.Username),
            Subject = email.Subject,
            Body = "<h1>Hello</h1>",
            IsBodyHtml = true,
        };

        mailMessage.To.Add("recipient");

        smtpClient.Send(mailMessage);
    }

    public void SendPasswordChangeEmail()
    {
        throw new NotImplementedException();
    }

    public void SendVerificationEmail()
    {
        throw new NotImplementedException();
    }
}
