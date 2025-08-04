using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.Emails;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.HttpUtils;
using MyPennyPincher_API_Tests.MailhogManager;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class EmailServiceTest : IDisposable
{
    private readonly HttpClient _client;
    private readonly IEmailService _emailService;
    private readonly MailHogManager _mailHogManager;
    private readonly IConfiguration _config;
    private readonly GeneralSettings _generalSettings;

    public EmailServiceTest() 
    {
        _generalSettings = new GeneralSettings 
        { 
            FrontendBaseUrl = "http://localhost:3000" 
        };

        var configData = new Dictionary<string, string?>
        {
            { "SmtpSettings:Host", "localhost" },
            { "SmtpSettings:Port", "1025" },
            { "SmtpSettings:EnableSsl", "false" },
            { "SmtpSettings:Username", "tewahido@email.com" },
            { "SmtpSettings:Password", "doesntmatter" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        var smtpOptions = new SmtpOptions();
        _config.GetSection("SmtpSettings").Bind(smtpOptions);

        var options = Options.Create(smtpOptions);

        _client = new HttpClient();
        
        _emailService = new EmailService(options);

        _mailHogManager = new MailHogManager();

        _mailHogManager.StartMailHog();
    }

    [Fact]
    public async Task GIVEN_MFAEmail_WHEN_SendingEmail_THEN_ReceiveEmailInMailHog()
    {
        //Arrange
        int code = 849509;
        MFAEmail mfaEmail = new MFAEmail(code);
        string toEmail = "test@email.com";
        _emailService.Send2FAEmail(mfaEmail, toEmail);

        //Act
        await Task.Delay(500);
        var response = await _client.GetAsync("http://localhost:8025/api/v2/messages");
        var content = await response.Content.ReadAsStringAsync();

        // Parse JSON
        var json = JObject.Parse(content);
        var items = (JArray)json["items"];
        Assert.True(items.Count > 0, "items should be greater than 0");

        var email = items.First;

        //Assert
        var to = email["To"][0];
        var recipient = $"{to["Mailbox"]}@{to["Domain"]}";
        Assert.Equal(recipient, toEmail);

        var subject = email["Content"]["Headers"]["Subject"][0].ToString();
        Assert.Equal(subject, mfaEmail.Subject);

        var body = email["Content"]["Body"].ToString();
        var decodedBody = EmailUtils.DecodeQuotedPrintable(body);
        Console.WriteLine(body);
        Assert.Contains(mfaEmail.Body, decodedBody);
    }

    [Fact]
    public async Task GIVEN_Verification_Email_WHEN_SendingEmail_THEN_ReceiveEmailInMailHog()
    {
        //Arrange
        UserAccessToken userAccessToken = new UserAccessToken { UserId = Guid.NewGuid(), Token = "edjneieig" };

        VerificationEmail verificationEmail = new VerificationEmail(userAccessToken, _generalSettings);
        
        string toEmail = "test@email.com";
        _emailService.SendVerificationEmail(verificationEmail, toEmail);

        //Act
        await Task.Delay(500);
        var response = await _client.GetAsync("http://localhost:8025/api/v2/messages");
        var content = await response.Content.ReadAsStringAsync();

        // Parse JSON
        var json = JObject.Parse(content);
        var items = (JArray)json["items"];
        Assert.True(items.Count > 0, "items should be greater than 0");

        var email = items.First;

        //Assert
        var to = email["To"][0];
        var recipient = $"{to["Mailbox"]}@{to["Domain"]}";
        Assert.Equal(recipient, toEmail);

        var subject = email["Content"]["Headers"]["Subject"][0].ToString();
        Assert.Equal(subject, verificationEmail.Subject);

        var body = email["Content"]["Body"].ToString();
        var decodedBody = Encoding.UTF8.GetString(Convert.FromBase64String(body));
        Assert.Contains(verificationEmail.Body, decodedBody);
    }

    public void Dispose()
    {
        _mailHogManager.RemoveAllMailHogContainers();
    }
}
