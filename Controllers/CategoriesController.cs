using FtelMarket.Data;
using FtelMarket.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FtelMarket.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll() =>
        await db.Categories
            .OrderBy(c => c.Id)
            .Select(c => new CategoryResponse(c.Id, c.Name))
            .ToListAsync();
}