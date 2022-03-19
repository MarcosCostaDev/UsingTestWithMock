using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Domain.Entities
{
    public class PokemonCaught
    {
        protected PokemonCaught() { }
        public PokemonCaught(Pokemon pokemon, string pokemonTrainerEmail)
        {
            PokemonId = pokemon.Id;
            PokemonTrainerEmail = pokemonTrainerEmail;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public int PokemonId { get; private set; }
        public string PokemonTrainerEmail { get; private set; }
    }
}
