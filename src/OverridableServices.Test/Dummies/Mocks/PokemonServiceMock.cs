using Moq;
using OverridableServices.Services;
using OverridableServices.Services.Interfaces;
using PokeApiNet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OverridableServices.Test.Dummies.Mocks
{
    public class PokemonServiceMock
    {

        public static Mock<IPokemonService> GetPokemonServiceMock()
        {
            var mock = new Mock<IPokemonService>();

            mock.Setup(x => x.GetPokemonByIdAsync(It.IsAny<int>()))
                .Returns(() => 
                Task.FromResult(new Pokemon
                {
                    Id = 1,
                    Name = "TestPokemon",
                    Height = 30
                }));

            mock.Setup(x => x.GetPokemonMovesAsync(It.IsAny<Pokemon>()))
                .ReturnsAsync(() => 
                    new List<Move> {
                        new Move
                        {
                            Id = 1,
                            Name = "TestMove",
                            Accuracy = 100
                        }
                    }
                    );

            mock.Setup(x => x.GetPokemonSpeciesAsync(It.IsAny<Pokemon>()))
               .Returns(async () =>
                    new PokemonSpecies
                    {
                        Id = 1,
                        Name = "TestSpecie",
                        CaptureRate = 1
                    }
               );

            return mock;
        }
    }
}
