using Microsoft.Extensions.Logging;
using OverridableServices.Services.Interfaces;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Services
{
    public class PokemonService : IPokemonService
    {
        private PokeApiClient _pokeApiClient;
        private ILogger<PokemonService> _logger;

        public PokemonService(PokeApiClient pokeApiClient, ILogger<PokemonService> logger)
        {
            _pokeApiClient = pokeApiClient;
            _logger = logger;
        }

        public Task<Pokemon> GetPokemonByIdAsync(int pokemonId)
        {

            return _pokeApiClient.GetResourceAsync<Pokemon>(pokemonId);
        }

        public async Task<IEnumerable<Move>> GetPokemonMovesAsync(Pokemon pokemon)
        {
            var moves = pokemon.Moves.Select(move => move.Move);
            return await _pokeApiClient.GetResourceAsync(moves);
        }

        public Task<PokemonSpecies> GetPokemonSpeciesAsync(Pokemon pokemon)
        {
            return _pokeApiClient.GetResourceAsync(pokemon.Species);
        }

        public async Task<Pokemon> GetRandomPokemonAsync(int min = 0, int max = 151)
        {
            var ramdomPokemonId = new Random().Next(min, max);
            var tries = 0;
            do
            {
                try
                {
                    return await GetPokemonByIdAsync(ramdomPokemonId);
                }
                catch (Exception)
                {
               
                    _logger.LogError($"Error when try get the pokemon number {ramdomPokemonId}");
                    tries++;
                }
               
            } while (tries < 3);

            return null;
        }
    }
}
