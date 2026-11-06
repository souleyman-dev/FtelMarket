namespace FtelMarket.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }   // toujours en UTC

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}