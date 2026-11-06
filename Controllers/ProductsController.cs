using FtelMarket.Data;
using FtelMarket.Dtos;
using FtelMarket.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FtelMarket.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(AppDbContext db) : ControllerBase
{
    private static ProductResponse ToResponse(Product p, string categoryName) =>
        new(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt, categoryName);

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAll() =>
        await db.Products
            .OrderBy(p => p.Id)
            .Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price,
                p.Stock, p.CategoryId, p.CreatedAt, p.Category.Name))
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(ProductRequest r)
    {
        var category = await db.Categories.FindAsync(r.CategoryId);
        if (category is null)
        {
            ModelState.AddModelError(nameof(r.CategoryId), "Catégorie inconnue.");
            return ValidationProblem(ModelState);
        }

        var p = new Product
        {
            Name = r.Name,
            Description = r.Description,
            Price = r.Price,
            Stock = r.Stock,
            CategoryId = r.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
        db.Products.Add(p);
        await db.SaveChangesAsync();

        return Created($"/api/products/{p.Id}", ToResponse(p, category.Name));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Update(int id, ProductRequest r)
    {
        var p = await db.Products.FindAsync(id);
        if (p is null) return NotFound();

        var category = await db.Categories.FindAsync(r.CategoryId);
        if (category is null)
        {
            ModelState.AddModelError(nameof(r.CategoryId), "Catégorie inconnue.");
            return ValidationProblem(ModelState);
        }

        p.Name = r.Name;
        p.Description = r.Description;
        p.Price = r.Price;
        p.Stock = r.Stock;
        p.CategoryId = r.CategoryId;
        await db.SaveChangesAsync();

        return ToResponse(p, category.Name);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await db.Products.FindAsync(id);
        if (p is null) return NotFound();

        db.Products.Remove(p);
        await db.SaveChangesAsync();
        return NoContent();
    }
}