using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Announcements;
public interface IAnnouncementRepository
{
	public Task<IEnumerable<Announcement>> ListAsync();

	public Task<Announcement> GetByIdAsync(Guid id);

	public Task<Guid> AddAsync(Announcement entity);

	public Task UpdateAsync(Announcement entity);

	public Task DeleteAsync(Guid id);
}
