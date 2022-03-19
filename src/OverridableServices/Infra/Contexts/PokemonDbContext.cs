using Microsoft.EntityFrameworkCore;
using OverridableServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices.Infra.Contexts
{
    public class PokemonDbContext : DbContext
    {
        public DbSet<PokemonCaught> PokemonCaught { get; set; }

        public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<PokemonCaught>(entity =>
            {
                entity.ToTable(name: "PokemonCaught");
            });

            base.OnModelCreating(builder);
        }
    }
}
