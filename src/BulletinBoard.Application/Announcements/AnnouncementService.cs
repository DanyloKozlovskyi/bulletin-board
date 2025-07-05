using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Announcements;
public class AnnouncementService : IAnnouncementService
{
	private readonly IAnnouncementRepository _repository;

	public AnnouncementService(IAnnouncementRepository repository)
	{
		_repository = repository;
	}

	public async Task<Guid> CreateAsync(AnnouncementCreateModel model)
	{
		var entity = model.ToAnnouncement();

		return await _repository.AddAsync(entity);
	}

	public async Task<IEnumerable<Announcement>> ListAsync(int? categoryId = null, int? subCategoryId = null)
	{
		var all = (await _repository.ListAsync()).ToList();

		if (categoryId.HasValue)
			all = all.Where(a => a.CategoryId == categoryId.Value).ToList();

		if (subCategoryId.HasValue)
			all = all.Where(a => a.SubCategoryId == subCategoryId.Value).ToList();

		return all;
	}

	public Task<Announcement> GetByIdAsync(Guid id)
	{
		return _repository.GetByIdAsync(id);
	}

	public async Task UpdateAsync(AnnouncementUpdateModel model)
	{
		var entity = model.ToAnnouncement();

		await _repository.UpdateAsync(entity);
	}

	public Task DeleteAsync(Guid id)
	{
		return _repository.DeleteAsync(id);
	}
}