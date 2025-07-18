namespace MyPennyPincher_API.Models.ConfigModels;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public int TokenValidityMins { get; set; }
}
