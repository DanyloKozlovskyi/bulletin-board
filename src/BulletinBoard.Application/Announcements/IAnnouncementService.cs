using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Announcements;
public interface IAnnouncementService
{
	Task<Guid> CreateAsync(AnnouncementCreateModel model);
	Task<IEnumerable<Announcement>> ListAsync(int? categoryId = null, int? subCategoryId = null);
	Task<Announcement> GetByIdAsync(Guid id);
	Task UpdateAsync(AnnouncementUpdateModel model);
	Task DeleteAsync(Guid id);
}