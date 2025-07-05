namespace BulletinBoard.Ui.Models.Announcements;
public static class AnnouncementExtensions
{
	public static AnnouncementUpdateViewModel ToUpdateModel(this AnnouncementViewModel model)
	{
		return new AnnouncementUpdateViewModel
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