using BulletinBoard.Ui.Features.Announcements.Models;

namespace BulletinBoard.Ui.Features.Announcements;
public interface IAnnouncementService
{
	Task<IEnumerable<Announcement>> ListAsync(int? categoryId = null, int? subCategoryId = null);
	Task<Announcement?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(AnnouncementCreateModel model);
	Task UpdateAsync(AnnouncementUpdateModel model);
	Task DeleteAsync(Guid id);
}