using System;
using GFTFluxoCaixa.Api.Configuration;
using GFTFluxoCaixa.Infrastructure.Data;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using GFTFluxoCaixa.Infrastructure.Data.Repository;
using GFTFluxoCaixa.Service;
using GFTFluxoCaixa.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GFTFluxoCaixa.Api
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
            services.AddMvc();
            services.AddControllers();
            services.AddCors();

            services.AddSingleton<DataContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwagger(Configuration);


            services.AddSingleton<IDatabaseSetup, DatabaseSetup>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "GFTFluxoCaixa Documentation");
                x.OAuthClientId("Swagger");
                x.OAuthClientSecret("swagger");
                x.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
            serviceProvider.GetService<IDatabaseSetup>().Setup();
        }
    }
}
