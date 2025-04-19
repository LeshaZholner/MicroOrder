using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MicroOrder.BasketService.Client;

public static class BasketServiceClientExtensions
{
    public const string BasketServiceClientName = "BasketServiceClient";

    public static IServiceCollection AddBasketService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IBasketServiceClient, BasketServiceClient>();
        services.Configure<BasketServiceClientOptions>(configuration.GetSection(BasketServiceClientOptions.SectionName));

        services.AddHttpClient(BasketServiceClientName, (sp, config) =>
        {
            var options = sp.GetRequiredService<IOptions<BasketServiceClientOptions>>().Value;

            config.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}
