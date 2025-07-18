namespace MyPennyPincher_API.Models.ConfigModels;

public class GeneralSettings
{
    public const string SectionName = "GeneralSettings";

    public required string FrontendBaseUrl { get; set; }
}
