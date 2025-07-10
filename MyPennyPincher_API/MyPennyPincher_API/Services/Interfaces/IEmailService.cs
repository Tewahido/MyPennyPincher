using MyPennyPincher_API.Models.Emails;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IEmailService
{
    void Send2FAEmail(MFAEmail email, string toEmail);
    void SendVerificationEmail(VerificationEmail email, string toEmail);
    void SendPasswordChangeEmail();
}
