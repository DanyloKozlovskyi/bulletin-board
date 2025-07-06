namespace BulletinBoard.Ui.Features.SubCategories;
public class SubCategoryService : ISubCategoryService
{
	private readonly ISubCategoryAdapter _repository;
	public SubCategoryService(ISubCategoryAdapter repository)
	{
		_repository = repository;
	}
	public Task<IEnumerable<SubCategory>> GetAllAsync(int? categoryId = null)
	{
		return _repository.GetAllAsync(categoryId).ContinueWith(t => (IEnumerable<SubCategory>)t.Result);
	}
	public Task<SubCategory?> GetByIdAsync(int id)
	{
		return _repository.GetByIdAsync(id);
	}
}
