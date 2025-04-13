using GreenDonut.Data;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[ObjectType<Brand>]
public static partial class BrandNode
{
    [UsePaging]
    [UseSorting]
    [UseFiltering]
    public static async Task<Connection<Product>> GetProductsAsync(
        [Parent] Brand brand,
        PagingArguments pagingArgs,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductsByBrandAsync(brand.Id, pagingArgs, cancellationToken).ToConnectionAsync();
    
    [NodeResolver]
    public static async Task<Brand?> GetBrandByIdAsync(
        int id,
        QueryContext<Brand> queryContext,
        BrandService brandService,
        CancellationToken cancellationToken)
        => await brandService.GetBrandByIdAsync(id, queryContext, cancellationToken);
}
