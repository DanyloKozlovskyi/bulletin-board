using BulletinBoard.Application.Categories;
using BulletinBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly ICategoryService _service;

	public CategoriesController(ICategoryService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Category>>> GetAll()
	{
		var categories = await _service.ListAsync();
		return Ok(categories);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Category>> GetById(int id)
	{
		var cat = await _service.GetByIdAsync(id);
		if (cat is null) return NotFound();
		return Ok(cat);
	}
}
