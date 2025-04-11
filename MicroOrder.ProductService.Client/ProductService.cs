using MicroOrder.ProductService.Data;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MicroOrder.ProductService.Client;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GetProductsResponse> GetProducts(CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(ProductServiceExtensions.ProductServiceHttpClient);

        var response = await httpClient.GetAsync("api/products", cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var responseData = RetriveResult<GetProductsResponse>(responseContent);

        return responseData ?? new GetProductsResponse { Products = [] };
    }

    public async Task<GetProductResponse> GetProduct(GetProductRequest request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(ProductServiceExtensions.ProductServiceHttpClient);

        var response = await httpClient.GetAsync($"api/products/{request.ProductId}", cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var responseData = RetriveResult<GetProductResponse>(responseContent);

        return responseData ?? new GetProductResponse { Product = new Product() };
    }

    public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(ProductServiceExtensions.ProductServiceHttpClient);
        var requestContent = RetriveRequestContent(request);

        var response = await httpClient.PostAsync("api/products", requestContent, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var responseData = RetriveResult<CreateProductResponse>(responseContent);

        return responseData ?? new CreateProductResponse { Product = new Product() };
    }

    public async Task<CheckAvailabilityProductItemRequest> CheckAvailability(CheckAvailabilityProductsRequest request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(ProductServiceExtensions.ProductServiceHttpClient);
        var requestContent = RetriveRequestContent(request);

        var response = await httpClient.PostAsync("api/products/check-availability", requestContent, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var responseData = RetriveResult<CheckAvailabilityProductItemRequest>(responseContent);

        return responseData ?? new CheckAvailabilityProductItemRequest();
    }

    private static TResult? RetriveResult<TResult>(string content)
    {
        var responseData = JsonSerializer.Deserialize<TResult>(content, _serializerOptions);

        return responseData ?? default;
    }

    private static StringContent? RetriveRequestContent<TRequest>(TRequest request)
    {
        return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);
    }
}
