using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.AppServices.Interfaces
{
    public interface ICatchPokemonAppService
    {
        Task<bool> CatchPokemonAsync(int pokemonId, string pokemonTrainerEmail);
    }
}
