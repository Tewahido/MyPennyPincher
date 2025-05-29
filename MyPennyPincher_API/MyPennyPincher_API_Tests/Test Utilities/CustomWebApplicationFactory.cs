using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;

namespace MyPennyPincher_API_Tests.Test_Utilities
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddAuthentication("FakeScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("FakeScheme", options => { });

                services.Configure<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme = "FakeScheme";
                    options.DefaultChallengeScheme = "FakeScheme";
                });

                services.AddAuthorization(); 
            });
        }
    }
}
