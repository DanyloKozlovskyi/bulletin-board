using BulletinBoard.Application.SubCategories;
using BulletinBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SubCategoriesController : ControllerBase
{
	private readonly ISubCategoryService _service;

	public SubCategoriesController(ISubCategoryService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<SubCategory>>> GetAll([FromQuery] int? categoryId)
	{
		var list = await _service.ListAsync(categoryId);
		return Ok(list);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<SubCategory>> GetById(int id)
	{
		var sub = await _service.GetByIdAsync(id);
		if (sub is null) return NotFound();
		return Ok(sub);
	}
}
