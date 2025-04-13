using GreenDonut.Data;

namespace eShop.Catalog.Services;

[DataLoaderGroup("CompanyBatchingContext")]
internal static class CompanyDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Company>> CompanyByIdAsync(
        IReadOnlyList<int> ids,
        QueryContext<Company> queryContext,
        CatalogContext context,
        CancellationToken ct)
        => await context.Companies
            .AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .With(queryContext.Include(t => t.Id))
            .ToDictionaryAsync(t => t.Id, ct);
}
