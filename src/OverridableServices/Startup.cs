using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OverridableServices.AppServices;
using OverridableServices.AppServices.Interfaces;
using OverridableServices.Infra.Contexts;
using OverridableServices.Services;
using OverridableServices.Services.Interfaces;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OverridableServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<PokemonDbContext>(context => context.UseSqlite(Configuration.GetConnectionString("PokemonDb")));

            services.AddScoped<PokeApiClient>();
            services.AddTransient<ICatchPokemonAppService, CatchPokemonAppService>();
            services.AddTransient<IPokemonService, PokemonService>();
            services.AddTransient<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
               
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            CreateDb(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }


        private void CreateDb(IApplicationBuilder app)
        {
            var connection = new SqliteConnectionStringBuilder(Configuration.GetConnectionString("PokemonDb"));
            try
            {
                if (File.Exists(connection.DataSource)) File.Delete(connection.DataSource);


                var connectionFolder = Directory.GetParent(connection.DataSource);
                if (!Directory.Exists(connectionFolder.FullName)) Directory.CreateDirectory(connectionFolder.FullName);


                var context = app.ApplicationServices.GetRequiredService<PokemonDbContext>();
                context.Database.EnsureCreated();


            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger>();
                logger.LogError(403, ex, "error on try to create db");
            }

            

        }
    }
}
