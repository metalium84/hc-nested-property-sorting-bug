using GreenDonut.Data;

namespace eShop.Catalog.Services;

public sealed class CompanyService(
    CatalogContext context, 
    ICompanyBatchingContext batching)
{
    public async Task<Company?> GetCompanyByIdAsync(int id,
        QueryContext<Company> queryContext,
        CancellationToken ct = default)
        => await batching.CompanyById.With(queryContext).LoadAsync(id, ct);
}
