namespace MyPennyPincher_API.Models.Emails;


public class MFAEmail
{
    public readonly string Subject = "MyPennyPincher - 2-Factor Authentication";
    
    public string Body { get; }

    public MFAEmail(int code)
    {
        Body = File.ReadAllText("2FA_Email_Template.html").Replace("{{CODE}}", code.ToString());
    }
}
