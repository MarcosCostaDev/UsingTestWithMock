using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OverridableServices.Test.Abstracts
{
    public class StartupFixture : WebApplicationFactory<Startup>
    {

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
           
            return base.CreateWebHostBuilder().UseEnvironment("test");
        }
    }
}
