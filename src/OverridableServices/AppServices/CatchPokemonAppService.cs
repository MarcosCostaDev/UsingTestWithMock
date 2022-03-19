using Microsoft.Extensions.Logging;
using OverridableServices.AppServices.Interfaces;
using OverridableServices.Domain.Entities;
using OverridableServices.Infra.Contexts;
using OverridableServices.Services.Interfaces;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.AppServices
{
    public class CatchPokemonAppService : ICatchPokemonAppService
    {
        private readonly ILogger<CatchPokemonAppService> _logger;
        private readonly PokemonDbContext _pokemonDbContext;
        private readonly IPokemonService _pokemonService;
        private readonly IEmailService _emailService;

        public CatchPokemonAppService(ILogger<CatchPokemonAppService> logger, PokemonDbContext pokemonDbContext, IPokemonService pokemonService,  IEmailService emailService)
        {
            _logger = logger;
            _pokemonDbContext = pokemonDbContext;
            _pokemonService = pokemonService;
            _emailService = emailService;
        }
        public async Task<bool> CatchPokemonAsync(int pokemonId, string pokemonTrainerEmail)
        {
            if (string.IsNullOrEmpty(pokemonTrainerEmail) || (pokemonId <= 0 || pokemonId > 151))  return false;

            try
            {
                var pokemon = await _pokemonService.GetPokemonByIdAsync(pokemonId);

                if (_pokemonDbContext.PokemonCaught.Any(p => p.PokemonTrainerEmail == pokemonTrainerEmail && p.PokemonId == pokemonId))
                {
                    _logger.LogInformation($"Pokemon {pokemon.Name} already cought by the trainer {pokemonTrainerEmail}");
                    return false;
                }

             
                var pokemonCaught = new PokemonCaught(pokemon, pokemonTrainerEmail);

                _pokemonDbContext.PokemonCaught.Add(pokemonCaught);

                await _emailService.SendPokemonCaughtAsync(pokemonCaught);

                _pokemonDbContext.SaveChanges();

                _logger.LogInformation($"Pokemon registered in {pokemonTrainerEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(403, ex, "Error on Add");
                return false;
            }
           
            return true;
        }
    }
}
