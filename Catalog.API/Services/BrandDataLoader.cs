using GreenDonut.Data;

namespace eShop.Catalog.Services;

[DataLoaderGroup("BrandBatchingContext")]
internal static class BrandDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Brand>> GetBrandByIdAsync(
        IReadOnlyList<int> ids,
        QueryContext<Brand> queryContext,
        CatalogContext context,
        CancellationToken ct)
        => await context.Brands
            .AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .With(queryContext.Include(t => t.Id))
            .ToDictionaryAsync(t => t.Id, ct);
    
    [DataLoader]
    public static async Task<Dictionary<string, Brand>> GetBrandByNameAsync(
        IReadOnlyList<string> names,
        CatalogContext context,
        CancellationToken ct)
        => await context.Brands
            .AsNoTracking()
            .Where(t => names.Contains(t.Name))
            .ToDictionaryAsync(t => t.Name, ct);
}
