using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MicroOrder.ProductService.Client;

public static class ProductServiceExtensions
{
    public const string ProductServiceHttpClient = "ProductService";

    public static IServiceCollection AddProductService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IProductService, ProductService>();
        services.Configure<ProductServiceOptions>(configuration.GetSection(ProductServiceOptions.SectionName));

        services.AddHttpClient(ProductServiceHttpClient, (sp, config) =>
        {
            var options = sp.GetRequiredService<IOptions<ProductServiceOptions>>().Value;

            config.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}
