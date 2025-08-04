using Microsoft.AspNetCore.WebUtilities;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.Models.Emails;

public class VerificationEmail
{
    public readonly string Subject = "MyPennyPincher - Account Verification";
    private readonly GeneralSettings _generalSettings;
    public string Body { get; }

    public VerificationEmail(UserAccessToken userAccessToken, GeneralSettings generalSettings)
    {
        _generalSettings = generalSettings;

        var link = GenerateVerificationLink(userAccessToken);

        Body = File.ReadAllText("Models/Emails/EmailTemplates/Verification_Email_Template.html").Replace("{{LINK}}", link);
    }

    private string GenerateVerificationLink(UserAccessToken userAccessToken)
    {
        var baseUrl = _generalSettings.FrontendBaseUrl + "/verify";

        var queryParams = new Dictionary<string, string?>
        {
            { "userId", userAccessToken.UserId.ToString()},
            { "token", userAccessToken.Token}
        };

        return QueryHelpers.AddQueryString(baseUrl, queryParams);
    }
}
