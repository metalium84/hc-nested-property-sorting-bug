using GreenDonut.Data;

namespace eShop.Catalog.Services;

public sealed class BrandService(
    CatalogContext context, 
    IBrandBatchingContext batching)
{
    public async Task<Brand?> GetBrandByIdAsync(int id,
        QueryContext<Brand> queryContext,
        CancellationToken ct = default)
        => await batching.BrandById.With(queryContext).LoadAsync(id, ct);
    
    public async Task<Brand?> GetBrandByNameAsync(
        string name, 
        CancellationToken ct = default)
        => await batching.BrandByName.LoadAsync(name, ct);

    public async Task<Page<Brand>> GetBrandsAsync(
        PagingArguments args, 
        CancellationToken ct = default) 
        => await context.Brands
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .ToPageAsync(args, ct);
}
