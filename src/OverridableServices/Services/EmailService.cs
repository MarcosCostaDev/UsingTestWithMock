using Microsoft.Extensions.Logging;
using OverridableServices.Domain.Entities;
using OverridableServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Services
{
    public class EmailService : IEmailService
    {
        private ILogger<EmailService> _logger;

        public EmailService (ILogger<EmailService> logger) {
            _logger = logger;
        }

        public Task<bool> SendPokemonCaughtAsync(PokemonCaught pokemonCaught)
        {
            _logger.LogInformation($"Email has been sent to {pokemonCaught.PokemonTrainerEmail} informing that pokemon {pokemonCaught.PokemonId} was caught!");

            return Task.FromResult(true);
        }
    }
}
