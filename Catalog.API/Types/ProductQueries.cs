using GreenDonut.Data;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[QueryType]
public static class ProductQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<Product>> GetProductsAsync(
        QueryContext<Product> context,
        PagingArguments pagingArgs,
        ProductService productService, 
        CancellationToken cancellationToken)
        => await productService.GetProductsAsync(context, pagingArgs, cancellationToken).ToConnectionAsync();
}
