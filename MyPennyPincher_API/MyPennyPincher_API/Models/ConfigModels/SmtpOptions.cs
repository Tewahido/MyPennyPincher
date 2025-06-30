namespace MyPennyPincher_API.Models.ConfigModels;

public class SmtpOptions
{
    public const string SmtpSection = "SmtpSettings";

    public string Host { get; }
    public int Port { get; }
    public bool EnableSsl { get;  }
    public string Username { get; }
    public string Password { get; }
}
