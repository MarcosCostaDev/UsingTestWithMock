using Moq;
using OverridableServices.Domain.Entities;
using OverridableServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverridableServices.Test.Dummies.Mocks
{
    public class EmailServiceMock
    {
        public static Mock<IEmailService> GetEmailServiceMock()
        {
            var mock = new Mock<IEmailService>();

            mock.Setup(x => x.SendPokemonCaughtAsync(It.IsAny<PokemonCaught>()))
                .Returns(() => Task.FromResult(true));

            return mock;
        }
    }
}
