using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDBContext.Data;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientesEProdutos.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configura os serviços da aplicação
        public void ConfigureServices(IServiceCollection services)
        {
            // Configura o DbContext com o Entity Framework Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // Adiciona suporte a controladores
            services.AddControllers();
        }

        // Configura o pipeline de requisição HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}