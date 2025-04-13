using GreenDonut.Data;

namespace eShop.Catalog.Types;

[ObjectType<Company>]
public static partial class CompanyNode
{
    [NodeResolver]
    public static async Task<Company?> GetCompanyByIdAsync(
        int id,
        QueryContext<Company> queryContext,
        CompanyService companyService,
        CancellationToken cancellationToken)
        => await companyService.GetCompanyByIdAsync(id, queryContext, cancellationToken);
}
