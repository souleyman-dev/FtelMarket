using System.ComponentModel.DataAnnotations;

namespace FtelMarket.Dtos;

public record ProductRequest(
    [Required, StringLength(200, MinimumLength = 1)] string Name,
    [StringLength(1000)] string? Description,
    [Range(typeof(decimal), "0.01", "1000000",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true)] decimal Price,
    [Range(0, int.MaxValue)] int Stock,
    [Range(1, int.MaxValue)] int CategoryId);

public record ProductResponse(
    int Id, string Name, string? Description, decimal Price,
    int Stock, int CategoryId, DateTime CreatedAt, string CategoryName);

public record CategoryResponse(int Id, string Name);