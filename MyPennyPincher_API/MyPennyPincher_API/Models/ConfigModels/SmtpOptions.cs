namespace MyPennyPincher_API.Models.ConfigModels;

public class SmtpOptions
{
    public const string SmtpSection = "SmtpSettings";

    public required string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
