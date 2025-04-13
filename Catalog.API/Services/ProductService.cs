using GreenDonut.Data;

namespace eShop.Catalog.Services;

public sealed class ProductService(
    CatalogContext context, 
    IProductBatchingContext batching)
{
    public async Task<Product?> GetProductByIdAsync(int id, QueryContext<Product>? queryContext,
        CancellationToken cancellationToken = default) 
        => await batching.ProductById.With(queryContext).LoadAsync(id, cancellationToken);

    public async Task<Page<Product>> GetProductsAsync(
        QueryContext<Product>? queryContext,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken = default) 
        => await context.Products
            .With(queryContext, Ordering.Ordering.DefaultOrder)
            .ToPageAsync(pagingArguments, cancellationToken);

    public async Task<Page<Product>?> GetProductsByBrandAsync(
        int brandId,
        PagingArguments pagingArgs,
        CancellationToken ct = default)
        => await batching.ProductsByBrandId.With(pagingArgs).LoadAsync(brandId, ct);

    public async Task<Page<Product>?> GetProductsByTypeAsync(
        int typeId,
        PagingArguments pagingArgs,
        CancellationToken ct = default)
        => await batching.ProductsByTypeId.With(pagingArgs).LoadAsync(typeId, ct);
}
