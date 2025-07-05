namespace BulletinBoard.Application.Announcements;
public class AnnouncementUpdateModel
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public bool Status { get; set; }
	public int CategoryId { get; set; }
	public int? SubCategoryId { get; set; }
}
