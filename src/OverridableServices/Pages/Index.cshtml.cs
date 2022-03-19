using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OverridableServices.AppServices.Interfaces;
using OverridableServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPokemonService _pokemonService;
        private readonly ICatchPokemonAppService _catchPokemonAppService;

        public IndexModel(ILogger<IndexModel> logger, IPokemonService pokemonService, ICatchPokemonAppService catchPokemonAppService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
            _catchPokemonAppService = catchPokemonAppService;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public PokemonSpecifications PokemonSpecifications { get; set; } = new PokemonSpecifications();

        [BindProperty]
        public string EmailTrainer { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public async Task OnPostRandomAsync()
        {
            ModelState.Clear();
            //ModelState.Remove("PokemonSpecifications");
            var pokemon = await _pokemonService.GetRandomPokemonAsync();
            var species = await _pokemonService.GetPokemonSpeciesAsync(pokemon);

            PokemonSpecifications = new PokemonSpecifications
            {
                PokemonId = pokemon.Id,
                Name = pokemon.Name,
                Color = species.Color.Name,
                Weight = pokemon.Weight
            };
        }

        public async Task OnPostCatchPokemonAsync()
        {
            var result = await _catchPokemonAppService.CatchPokemonAsync(PokemonSpecifications.PokemonId, EmailTrainer);

            if(result)
            {
                Message = $"The pokemon {PokemonSpecifications.Name} was cought!";
            }
            else 
            {
                ModelState.AddModelError("", "Error on catch pokemon");
                ErrorMessage = $"The pokemon {PokemonSpecifications.Name} was not cought.";
            }
            this.ModelState.Remove("PokemonSpecifications");
        }
    }


    public class PokemonSpecifications
    {
        [BindProperty]
        public int PokemonId { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Color { get; set; }
        [BindProperty]
        public int Weight { get; set; }
    }
}
