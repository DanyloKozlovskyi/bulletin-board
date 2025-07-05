namespace BulletinBoard.Application.Announcements;
public class AnnouncementCreateModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public bool Status { get; set; } = true;
	public int CategoryId { get; set; }
	public int? SubCategoryId { get; set; }
}
