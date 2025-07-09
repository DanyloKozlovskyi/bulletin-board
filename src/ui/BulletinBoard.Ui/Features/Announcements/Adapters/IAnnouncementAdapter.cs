using BulletinBoard.Ui.Features.Announcements.Models;

namespace BulletinBoard.Ui.Features.Announcements.Adapters;
public interface IAnnouncementAdapter
{
	Task<List<Announcement>> GetAllAsync(int? categoryId = null, int? subCategoryId = null);
	Task<Announcement?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(AnnouncementCreateModel model);
	Task UpdateAsync(AnnouncementUpdateModel model);
	Task DeleteAsync(Guid id);
}
