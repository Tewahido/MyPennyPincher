using System;

public class JwtOptions
{
    public const string JwtSection = "Jwt";

    public string Key { get; set; }
    public string Issuer { get; set; }
    public int TokenValidityMins { get; set; }
}
