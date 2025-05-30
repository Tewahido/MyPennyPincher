using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MyPennyPincher_API_Tests.WebApplicationFactory
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string InMemoryDbName = $"TestDb_{Guid.NewGuid().ToString()}";
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions>();
                services.RemoveAll<MyPennyPincherDbContext>();

                var options = services.Where(s => s.ServiceType.BaseType == typeof(DbContextOptions<MyPennyPincherDbContext>)).ToList();
                
                foreach (var option in options)
                {
                    services.Remove(option);
                }

                services.AddDbContext<MyPennyPincherDbContext>(options =>
                {
                    options.UseInMemoryDatabase(InMemoryDbName);
                });

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
