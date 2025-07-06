using BulletinBoard.Ui.Features.Announcements.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Ui.Features.Announcements.Models.ActionModels;
public class AnnouncementIndexViewModel
{
	public IList<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
	public IList<SelectListItem> SubCategories { get; set; } = new List<SelectListItem>();
	public IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();

	public int? CategoryId { get; set; }
	public int? SubCategoryId { get; set; }
}
