using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using OverridableServices.AppServices.Interfaces;
using OverridableServices.Domain.Entities;
using OverridableServices.Services.Interfaces;
using OverridableServices.Test.Abstracts;
using OverridableServices.Test.Dummies.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace OverridableServices.Test.AppServices
{
    public class CatchPokemonAppServiceTest : IClassFixture<StartupFixture>
    {
        private StartupFixture _startupFixture;

        public CatchPokemonAppServiceTest(StartupFixture startupFixture)
        {
            _startupFixture = startupFixture;
        }


        [Fact]
        public async Task CatchPokemonAsync_with_valid_email_returnsTrue()
        {
            var pokemonServiceMock = PokemonServiceMock.GetPokemonServiceMock();
            var emailServiceMock = EmailServiceMock.GetEmailServiceMock();

            var mockServices = _startupFixture
                .WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(ServiceDescriptor.Transient<IPokemonService>(p => pokemonServiceMock.Object));
                    services.Replace(ServiceDescriptor.Transient<IEmailService>(p => emailServiceMock.Object));
                }));

            var appService = mockServices.Services.GetRequiredService<ICatchPokemonAppService>();

            var result = await appService.CatchPokemonAsync(3, "email@test.com");

            Assert.True(result);
            pokemonServiceMock.Verify(p => p.GetPokemonByIdAsync(It.IsAny<int>()), Times.Once);
            emailServiceMock.Verify(p => p.SendPokemonCaughtAsync(It.IsAny<PokemonCaught>()), Times.Once);
        }
    }
}
