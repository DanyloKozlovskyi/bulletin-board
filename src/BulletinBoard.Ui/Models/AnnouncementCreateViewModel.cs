namespace BulletinBoard.Ui.Models;

public class AnnouncementCreateViewModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public bool Status { get; set; }
	public int CategoryId { get; set; }
	public int? SubCategoryId { get; set; }
}
