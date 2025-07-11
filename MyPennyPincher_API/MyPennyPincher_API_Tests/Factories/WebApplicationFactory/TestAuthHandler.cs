﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace MyPennyPincher_API_Tests.Factories.WebApplicationFactory;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, new Guid("b7a5d653-3f5a-4a69-a732-4a3e0f40e8a3").ToString()),
            new Claim(ClaimTypes.Name, "Test User Full Name")
        };

        var identity = new ClaimsIdentity(claims, "FakeScheme");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "FakeScheme");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
