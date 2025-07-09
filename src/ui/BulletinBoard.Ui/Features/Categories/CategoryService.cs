namespace BulletinBoard.Ui.Features.Categories;
public class CategoryService : ICategoryService
{
	private readonly ICategoryAdapter _repository;
	public CategoryService(ICategoryAdapter repository)
	{
		_repository = repository;
	}

	public Task<IEnumerable<Category>> GetAllAsync()
	{
		return _repository.GetAllAsync();
	}

	public Task<Category?> GetByIdAsync(int id)
	{
		return _repository.GetByIdAsync(id);
	}
}