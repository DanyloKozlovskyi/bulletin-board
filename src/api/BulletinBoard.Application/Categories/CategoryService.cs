using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Categories;
public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository _repository;

	public CategoryService(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<Category>> ListAsync()
	{
		return await _repository.ListAsync();
	}

	public async Task<Category?> GetByIdAsync(int id)
	{
		var c = await _repository.GetByIdAsync(id);
		return c;
	}
}
