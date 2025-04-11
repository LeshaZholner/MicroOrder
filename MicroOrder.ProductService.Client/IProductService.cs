using MicroOrder.ProductService.Data;

namespace MicroOrder.ProductService.Client;

public interface IProductService
{
    Task<GetProductResponse> GetProduct(GetProductRequest request, CancellationToken cancellationToken);
    Task<GetProductsResponse> GetProducts(CancellationToken cancellationToken);
    Task<CreateProductResponse> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken);
    Task<CheckAvailabilityProductItemRequest> CheckAvailability(CheckAvailabilityProductsRequest request, CancellationToken cancellationToken);
}
