namespace MyPennyPincher_API.Models.ConfigModels;

public class SlidingRateLimitOptions
{
    public const string SlidingRateLimitSection = "SlidingRateLimit";

    public int PermitLimit { get; set; }
    public int Window { get; set; }
    public int SegmentsPerWindow { get; set; }
    public int QueueLimit { get; set; }
}