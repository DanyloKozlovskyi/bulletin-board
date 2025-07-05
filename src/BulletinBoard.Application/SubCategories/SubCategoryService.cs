using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.SubCategories;
public class SubCategoryService : ISubCategoryService
{
	private readonly ISubCategoryRepository _repository;

	public SubCategoryService(ISubCategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<SubCategory>> ListAsync()
	{
		return await _repository.ListAsync();
	}

	public async Task<SubCategory?> GetByIdAsync(int id)
	{
		var s = await _repository.GetByIdAsync(id);
		return s;
	}
}
