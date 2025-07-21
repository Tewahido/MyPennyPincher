namespace MyPennyPincher_API.Models.ConfigModels;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public int TokenValidityMins { get; set; }
}
