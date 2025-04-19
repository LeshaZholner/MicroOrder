using MicroOrder.BasketService.Contracts;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MicroOrder.BasketService.Client;

public class BasketServiceClient : IBasketServiceClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public BasketServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CustomerBasketResponse?> GetBasket()
    {
        var httpClient = _httpClientFactory.CreateClient(BasketServiceClientExtensions.BasketServiceClientName);

        var response = await httpClient.GetAsync($"api/basket");
        var responseContent = await response.Content.ReadAsStringAsync();

        var result = RetriveResult<CustomerBasketResponse>(responseContent);

        return result;
    }

    public async Task<CustomerBasketResponse?> AddItem(AddBasketItemRequest basketItem)
    {
        var httpClient = _httpClientFactory.CreateClient(BasketServiceClientExtensions.BasketServiceClientName);

        var requetContent = RetriveRequestContent(basketItem);

        var response = await httpClient.PostAsync($"api/basket/additem", requetContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        var result = RetriveResult<CustomerBasketResponse>(responseContent);

        return result;
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
