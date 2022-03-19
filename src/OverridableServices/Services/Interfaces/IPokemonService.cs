using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemonByIdAsync(int pokemonId);

        Task<PokemonSpecies> GetPokemonSpeciesAsync(Pokemon pokemon);

        Task<IEnumerable<Move>> GetPokemonMovesAsync(Pokemon pokemon);

        Task<Pokemon> GetRandomPokemonAsync(int min = 0, int max = 151);
    }
}
