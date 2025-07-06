namespace BulletinBoard.Ui.Features.Announcements.Models;
public static class AnnouncementExtensions
{
	public static AnnouncementUpdateModel ToUpdateModel(this Announcement model)
	{
		return new AnnouncementUpdateModel
		{
			Id = model.Id,
			Title = model.Title,
			Description = model.Description,
			Status = model.Status,
			CategoryId = model.CategoryId,
			SubCategoryId = model.SubCategoryId
		};
	}
}