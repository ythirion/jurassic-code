using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JurassicCode.API;
using JurassicCode.Db2;

namespace JurassicCode.Tests.Integration
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace the real database with a clean test database
                services.AddScoped<IParkService>(provider =>
                {
                    var parkService = new ParkService();
                    DataAccessLayer.Init(new Database());
                    return parkService;
                });
            });
        }
    }
}