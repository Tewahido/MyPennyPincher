namespace MyPennyPincher_API.Models.ConfigModels;

public class SmtpOptions
{
    public const string SectionName = "SmtpSettings";

    public string? Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
