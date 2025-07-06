using BulletinBoard.Ui.Features.Announcements;
using BulletinBoard.Ui.Features.Announcements.Models;
using BulletinBoard.Ui.Features.Announcements.Models.ActionModels;
using BulletinBoard.Ui.Features.Categories;
using BulletinBoard.Ui.Features.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Ui.Controllers;
public class AnnouncementsController : Controller
{
	private readonly IAnnouncementService _service;
	private readonly ICategoryService _categoryService;
	private readonly ISubCategoryService _subCategoryService;

	public AnnouncementsController(IHttpClientFactory http, IAnnouncementService service, ICategoryService categoryService, ISubCategoryService subCategoryService)
	{
		_service = service;
		_categoryService = categoryService;
		_subCategoryService = subCategoryService;
	}

	public async Task<IActionResult> Index(int? categoryId, int? subCategoryId)
	{
		var announcements = await _service.ListAsync(categoryId, subCategoryId);

		var categories = await _categoryService.GetAllAsync();
		var categoryItems = categories
			.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
			.Prepend(new SelectListItem("All Categories", ""))
			.ToList();

		var subCategories = await _subCategoryService.GetAllAsync(categoryId);
		var subcategoryItems = subCategories
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

		await PopulateFormListsAsync();

		return View(vm);
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var vm = await _service.GetByIdAsync(id);
		if (vm == null) return NotFound();
		return View(vm);
	}

	private async Task PopulateFormListsAsync(int? categoryId = null)
	{
		var cats = await _categoryService.GetAllAsync();
		ViewBag.FormCategories = cats
			.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
			.ToList();

		var subs = categoryId.HasValue
			? await _subCategoryService.GetAllAsync(categoryId)
			: new List<SubCategory>();
		ViewBag.FormSubCategories = subs
			.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
			.ToList();
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		await PopulateFormListsAsync();
		return View(new AnnouncementCreateModel());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(AnnouncementCreateModel m)
	{
		if (!ModelState.IsValid)
		{
			await PopulateFormListsAsync(m.CategoryId);
			return View(m);
		}
		await _service.CreateAsync(m);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var existing = await _service.GetByIdAsync(id);
		if (existing is null) return NotFound();

		var vm = existing.ToUpdateModel();
		await PopulateFormListsAsync(vm.CategoryId);
		return PartialView("_AnnouncementEditForm", vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AnnouncementUpdateModel m)
	{
		if (!ModelState.IsValid)
		{
			await PopulateFormListsAsync(m.CategoryId);
			return View(m);
		}
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