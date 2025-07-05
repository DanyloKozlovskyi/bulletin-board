namespace BulletinBoard.Ui.Models;
public class AnnouncementViewModel
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime CreatedDate { get; set; }
	public bool Status { get; set; }
	public int CategoryId { get; set; }
	public string CategoryName { get; set; }
	public int? SubCategoryId { get; set; }
	public string SubCategoryName { get; set; }
}
