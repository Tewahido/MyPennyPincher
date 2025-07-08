using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using MyPennyPincher_API.Models.Emails;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Factories.WebApplicationFactory;
using MyPennyPincher_API_Tests.MailhogManager;
using Newtonsoft.Json.Linq;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class EmailServiceTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;
    private readonly IEmailService _emailService;
    private readonly MailHogManager _mailHogManager;

    public EmailServiceTest( CustomWebApplicationFactory<Program> factory) 
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async void GIVEN_MFAEmail_WHEN_SendingEmail_THEN_ReceiveEmailInMailHog()
    {
        //Arrange
        MFAEmail mfaEmail = new MFAEmail(849509);
        string toEmail = "test@email.com";

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
        Assert.Equal(body, mfaEmail.Body);
    } 
    

    public void Dispose()
    {
        _mailHogManager.RemoveAllMailHogContainers();
    }
}
