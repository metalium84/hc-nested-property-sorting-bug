using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.Models;

public class Company
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}
