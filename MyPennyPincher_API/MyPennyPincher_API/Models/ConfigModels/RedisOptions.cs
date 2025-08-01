namespace MyPennyPincher_API.Models.ConfigModels;

public class RedisOptions
{
    public const string SectionName = "RedisOptions";

    public string Connection { get; set; }
}
