using BulletinBoard.Ui.Features.Announcements.Adapters;
using BulletinBoard.Ui.Features.Announcements.Models;

namespace BulletinBoard.Ui.Features.Announcements;
public class AnnouncementService : IAnnouncementService
{
	private readonly IAnnouncementAdapter _repository;

	public AnnouncementService(IAnnouncementAdapter repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<Announcement>> ListAsync(int? categoryId = null, int? subCategoryId = null)
	{
		var items = await _repository.GetAllAsync(categoryId, subCategoryId);
		return items;
	}

	public async Task<Announcement?> GetByIdAsync(Guid id)
	{
		var a = await _repository.GetByIdAsync(id);
		return a;
	}

	public async Task<Guid> CreateAsync(AnnouncementCreateModel model)
	{
		return await _repository.CreateAsync(model);
	}

	public async Task UpdateAsync(AnnouncementUpdateModel model)
	{
		await _repository.UpdateAsync(model);
	}

	public Task DeleteAsync(Guid id)
	{
		return _repository.DeleteAsync(id);
	}
}
