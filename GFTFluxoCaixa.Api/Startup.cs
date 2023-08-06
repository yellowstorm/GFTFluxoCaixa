using System;
using System.Text.Json.Serialization;
using GFTFluxoCaixa.Api.Configuration;
using GFTFluxoCaixa.Infrastructure.CrossCutting;
using GFTFluxoCaixa.Infrastructure.Data;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using GFTFluxoCaixa.Infrastructure.Data.Repository;
using GFTFluxoCaixa.Service;
using GFTFluxoCaixa.Service.Interface;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            services.AddSingleton<DataContext>();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                x.JsonSerializerOptions.IncludeFields = true;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHealthChecks()
                .AddSqlite(sqliteConnectionString: Configuration.GetConnectionString("Api_ConnectionString"), healthQuery: "SELECT 1;", name: "Sqlite", failureStatus: HealthStatus.Degraded,
                 tags: new string[] { "db", "sql", "sqlite" });

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(5);
                options.MaximumHistoryEntriesPerEndpoint(10);
                options.AddHealthCheckEndpoint("API com Health Checks", "/health");
            })
.AddInMemoryStorage();
            services.AddSwagger(Configuration);


            services.AddSingleton<IDatabaseSetup, DatabaseSetup>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ITipoContaRepository, TipoContaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ITipoContaService, TipoContaService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IFluxoCaixaService, FluxoCaixaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlerMiddleware>();

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


            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => { options.UIPath = "/dashboard"; });

            serviceProvider.GetService<IDatabaseSetup>().Setup();
        }
    }
}
