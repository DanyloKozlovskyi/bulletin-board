using BulletinBoard.Ui.Models.Announcements;
using BulletinBoard.Ui.Models.Announcements.ActionModels;
using BulletinBoard.Ui.Models.Categories;
using BulletinBoard.Ui.Models.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Ui.Controllers;
public class AnnouncementsController : Controller
{
	private readonly HttpClient _client;

	public AnnouncementsController(IHttpClientFactory http)
	{
		_client = http.CreateClient("BulletinBoardApi");
	}

	public async Task<IActionResult> Index(int? categoryId, int? subCategoryId)
	{
		var query = new List<string>();
		if (categoryId.HasValue) query.Add($"categoryId={categoryId}");
		if (subCategoryId.HasValue) query.Add($"subCategoryId={subCategoryId}");
		var url = "announcements"
				  + (query.Any() ? "?" + string.Join("&", query) : "");
		var announcements = await _client.GetFromJsonAsync<List<AnnouncementViewModel>>(url)
							   ?? new List<AnnouncementViewModel>();

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
			SelectedCategoryId = categoryId,
			SelectedSubCategoryId = subCategoryId
		};

		return View(vm);
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var vm = await _client.GetFromJsonAsync<AnnouncementViewModel>($"announcements/{id}");
		if (vm == null) return NotFound();
		return View(vm);
	}

	[HttpGet]
	public IActionResult Create() => View(new AnnouncementCreateViewModel());

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(AnnouncementCreateViewModel m)
	{
		if (!ModelState.IsValid) return View(m);

		await _client.PostAsJsonAsync("announcements", m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var existing = await _client.GetFromJsonAsync<AnnouncementViewModel>($"announcements/{id}");
		if (existing == null) return NotFound();

		var vm = existing.ToUpdateModel();

		return View(vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AnnouncementUpdateViewModel m)
	{
		if (!ModelState.IsValid) return View(m);

		await _client.PutAsJsonAsync($"announcements/{m.Id}", m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Delete(Guid id)
	{
		var vm = await _client.GetFromJsonAsync<AnnouncementViewModel>($"announcements/{id}");
		if (vm == null) return NotFound();
		return View(vm);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		await _client.DeleteAsync($"announcements/{id}");
		return RedirectToAction(nameof(Index));
	}
}