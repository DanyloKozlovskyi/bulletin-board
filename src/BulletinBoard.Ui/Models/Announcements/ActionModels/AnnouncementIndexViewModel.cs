using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Ui.Models.Announcements.ActionModels;
public class AnnouncementIndexViewModel
{
	public IList<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
	public IList<SelectListItem> SubCategories { get; set; } = new List<SelectListItem>();
	public IList<AnnouncementViewModel> Announcements { get; set; } = new List<AnnouncementViewModel>();

	public int? CategoryId { get; set; }
	public int? SubCategoryId { get; set; }
}
