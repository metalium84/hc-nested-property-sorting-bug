using System.Text.Json;
using Npgsql;
using Path = System.IO.Path;

namespace eShop.Catalog.Migrations;

public sealed class CatalogContextSeed(
    IWebHostEnvironment env,
    ILogger<CatalogContextSeed> logger) 
    : IDbSeeder<CatalogContext>
{
    public async Task SeedAsync(CatalogContext context)
    {
        var contentRootPath = env.ContentRootPath;
        var picturePath = env.WebRootPath;

        // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
        await context.Database.OpenConnectionAsync();
        await ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypesAsync();

        if (!context.Products.Any())
        {
            var sourcePath = Path.Combine(contentRootPath, "Migrations", "catalog.json");
            var sourceJson = await File.ReadAllTextAsync(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<ProductEntry[]>(sourceJson)!;

            context.ProductTypes.RemoveRange(context.ProductTypes);
            await context.ProductTypes.AddRangeAsync(
                sourceItems.Select(x => x.Type).Distinct().Select(typeName => new ProductType { Name = typeName, }));
            logger.LogInformation("Seeded catalog with {NumTypes} types", context.ProductTypes.Count());
            
            context.Companies.RemoveRange(context.Companies);
            await context.Companies.AddRangeAsync(
                sourceItems.Select(x => x.Company).Distinct().Select(companyName => new Company { Name = companyName, }));
            logger.LogInformation("Seeded catalog with {NumCompanies} types", context.Companies.Count());

            await context.SaveChangesAsync();

            var typeIdsByName = await context.ProductTypes.ToDictionaryAsync(x => x.Name, x => x.Id);
            var companyIdsByName = await context.Companies.ToDictionaryAsync(x => x.Name, x => x.Id);
            
            context.Brands.RemoveRange(context.Brands);
            await context.Brands.AddRangeAsync(
                sourceItems.Select(x => new { x.Brand, x.Company}).DistinctBy(x => x.Brand).Select(brand => new Brand { Name = brand.Brand, CompanyId = companyIdsByName[brand.Company]}));
            logger.LogInformation("Seeded catalog with {NumBrands} brands", context.Brands.Count());
            
            await context.SaveChangesAsync();
            
            var brandIdsByName = await context.Brands.ToDictionaryAsync(x => x.Name, x => x.Id);

            await context.Products.AddRangeAsync(
                sourceItems.Select(source => new Product
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Price = source.Price,
                    BrandId = brandIdsByName[source.Brand],
                    TypeId = typeIdsByName[source.Type],
                    AvailableStock = 100,
                    MaxStockThreshold = 200,
                    RestockThreshold = 10,
                    ImageFileName = $"images/{source.Id}.webp",
                }));

            logger.LogInformation("Seeded catalog with {NumItems} items", context.Products.Count());
            await context.SaveChangesAsync();
        }
    }

    private sealed class ProductEntry
    {
        public required int Id { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Company { get; set; }
    }
}
