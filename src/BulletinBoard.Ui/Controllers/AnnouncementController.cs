using BulletinBoard.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Ui.Controllers;
public class AnnouncementController : Controller
{
	private readonly HttpClient _client;

	public AnnouncementController(IHttpClientFactory http)
	{
		_client = http.CreateClient("BulletinBoardApi");
	}

	public async Task<IActionResult> Index(int? categoryId, int? subCategoryId)
	{
		var query = new List<string>();
		if (categoryId.HasValue) query.Add($"categoryId={categoryId}");
		if (subCategoryId.HasValue) query.Add($"subCategoryId={subCategoryId}");
		var url = "api/announcement" + (query.Count > 0 ? "?" + string.Join("&", query) : "");

		var list = await _client.GetFromJsonAsync<List<AnnouncementViewModel>>(url);
		return View(list);
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var vm = await _client.GetFromJsonAsync<AnnouncementViewModel>($"api/announcement/{id}");
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

		await _client.PostAsJsonAsync("api/announcement", m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var existing = await _client.GetFromJsonAsync<AnnouncementViewModel>($"api/announcement/{id}");
		if (existing == null) return NotFound();

		var vm = existing.ToUpdateModel();

		return View(vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AnnouncementUpdateViewModel m)
	{
		if (!ModelState.IsValid) return View(m);

		await _client.PutAsJsonAsync($"api/announcement/{m.Id}", m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Delete(Guid id)
	{
		var vm = await _client.GetFromJsonAsync<AnnouncementViewModel>($"api/announcement/{id}");
		if (vm == null) return NotFound();
		return View(vm);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		await _client.DeleteAsync($"api/announcement/{id}");
		return RedirectToAction(nameof(Index));
	}
}