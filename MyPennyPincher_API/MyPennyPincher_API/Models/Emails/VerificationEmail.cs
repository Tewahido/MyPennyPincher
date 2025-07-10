namespace MyPennyPincher_API.Models.Emails;

public class VerificationEmail
{
    public readonly string Subject = "MyPennyPincher - Account Verification";
    public string Body { get; }

    public VerificationEmail(int code)
    {
        Body = File.ReadAllText("Verification_Email_Template.html").Replace("{{CODE}}", code.ToString());
    }
}
