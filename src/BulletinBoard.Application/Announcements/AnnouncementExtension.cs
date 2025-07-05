using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Announcements;
public static class AnnouncementExtension
{
	public static Announcement ToAnnouncement(this AnnouncementCreateModel model)
	{
		return new Announcement
		{
			Id = Guid.NewGuid(),
			Title = model.Title,
			Description = model.Description,
			Status = model.Status,
			CategoryId = model.CategoryId,
			SubCategoryId = model.SubCategoryId
		};
	}

	public static Announcement ToAnnouncement(this AnnouncementUpdateModel model)
	{
		return new Announcement
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
