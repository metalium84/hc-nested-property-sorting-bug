using GreenDonut.Data;

namespace eShop.Catalog.Ordering;

internal static class Ordering
{
    public static SortDefinition<Product> DefaultOrder(SortDefinition<Product> sortDefinition) 
        => sortDefinition
            .IfEmpty(d => d.AddAscending(p => p.Name))
            .AddAscending(p => p.Id);
}
