using BulletinBoard.Ui.Features.Announcements.Models;
using BulletinBoard.Ui.Features.Announcements.Models.ActionModels;
using BulletinBoard.Ui.Features.Categories;
using BulletinBoard.Ui.Features.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Ui.Features.Announcements;
public class AnnouncementsController : Controller
{
	private readonly HttpClient _client;
	private readonly IAnnouncementService _service;

	public AnnouncementsController(IHttpClientFactory http, IAnnouncementService service)
	{
		_service = service;
		_client = http.CreateClient("BulletinBoardApi");
	}

	public async Task<IActionResult> Index(int? categoryId, int? subCategoryId)
	{
		//var query = new List<string>();
		//if (categoryId.HasValue) query.Add($"categoryId={categoryId}");
		//if (subCategoryId.HasValue) query.Add($"subCategoryId={subCategoryId}");
		//var url = "announcements"
		//		  + (query.Any() ? "?" + string.Join("&", query) : "");
		//var announcements = await _client.GetFromJsonAsync<List<Announcement>>(url)
		//					   ?? new List<Announcement>();
		var announcements = await _service.ListAsync(categoryId, subCategoryId);


		var categories = await _client.GetFromJsonAsync<List<CategoryViewModel>>("categories")
						  ?? new List<CategoryViewModel>();
		var categoryItems = categories
			.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
			.Prepend(new SelectListItem("All Categories", ""))
			.ToList();

		List<SubCategoryViewModel> subcategories = new();
		if (categoryId.HasValue)
		{
			subcategories = await _client
				.GetFromJsonAsync<List<SubCategoryViewModel>>($"subcategories?categoryId={categoryId}")
				?? new List<SubCategoryViewModel>();
		}
		var subcategoryItems = subcategories
			.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
			.Prepend(new SelectListItem("All SubCategories", ""))
			.ToList();

		var vm = new AnnouncementIndexViewModel
		{
			Announcements = announcements,
			Categories = categoryItems,
			SubCategories = subcategoryItems,
			CategoryId = categoryId,
			SubCategoryId = subCategoryId
		};

		return View(vm);
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var vm = await _service.GetByIdAsync(id);
		if (vm == null) return NotFound();
		return View(vm);
	}

	[HttpGet]
	public IActionResult Create() => View(new AnnouncementCreateModel());

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(AnnouncementCreateModel m)
	{
		if (!ModelState.IsValid) return View(m);

		await _service.CreateAsync(m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var existing = await _service.GetByIdAsync(id);
		if (existing == null) return NotFound();

		var vm = existing.ToUpdateModel();

		return View(vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AnnouncementUpdateModel m)
	{
		if (!ModelState.IsValid) return View(m);

		await _service.UpdateAsync(m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Delete(Guid id)
	{
		var vm = await _service.GetByIdAsync(id);
		if (vm == null) return NotFound();
		return View(vm);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		await _service.DeleteAsync(id);
		return RedirectToAction(nameof(Index));
	}
}