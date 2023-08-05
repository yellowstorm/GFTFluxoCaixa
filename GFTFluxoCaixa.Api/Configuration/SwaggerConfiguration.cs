
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GFTFluxoCaixa.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "GFTFluxoCaixa - API",
                    Description = "API do Desafio GFT de Fluxo de Caixa"
                });
            });

            return services;
        }
    }
}
