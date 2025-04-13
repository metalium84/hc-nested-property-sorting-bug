// ReSharper disable CollectionNeverUpdated.Global
using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.Models;

public sealed class Brand
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public int CompanyId { get; set; }

    public Company? Company { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
