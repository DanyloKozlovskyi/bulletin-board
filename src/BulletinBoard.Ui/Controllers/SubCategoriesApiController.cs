using BulletinBoard.Ui.Features.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Ui.Controllers;
[Route("[controller]/[action]")]
public class SubCategoriesApiController : Controller
{
	private readonly ISubCategoryService _svc;
	public SubCategoriesApiController(ISubCategoryService svc)
		=> _svc = svc;

	// GET /SubCategoriesApi/GetByCategory?categoryId=5
	[HttpGet]
	public async Task<IActionResult> GetByCategory(int categoryId)
	{
		var list = await _svc.GetAllAsync(categoryId);

		var json = list
			.Select(s => new { id = s.Id, name = s.Name })
			.ToList();

		return Json(json);
	}
}