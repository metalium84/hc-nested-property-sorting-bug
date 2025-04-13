using GreenDonut.Data;

namespace eShop.Catalog.Types;

[ObjectType<Product>]
public static partial class ProductNode
{
    static partial void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Ignore(t => t.BrandId)
            .Ignore(t => t.TypeId)
            .Ignore(t => t.AddStock(default))
            .Ignore(t => t.RemoveStock(default));
    }

    public static async Task<Brand?> GetBrandAsync(
        [Parent(requires: nameof(Product.BrandId))] Product product,
        QueryContext<Brand> queryContext,
        BrandService brandService, 
        CancellationToken cancellationToken)
        => await brandService.GetBrandByIdAsync(product.BrandId, queryContext, cancellationToken);
    
    public static async Task<ProductType?> GetTypeAsync(
        [Parent] Product product, 
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypeByIdAsync(product.BrandId, cancellationToken);
    
    [NodeResolver]
    public static async Task<Product?> GetProductByIdAsync(
        int id,
        QueryContext<Product> queryContext,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductByIdAsync(id, queryContext, cancellationToken);
}
