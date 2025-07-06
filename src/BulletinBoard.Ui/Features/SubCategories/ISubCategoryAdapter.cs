namespace BulletinBoard.Ui.Features.SubCategories;
public interface ISubCategoryAdapter
{
	Task<List<SubCategory>> GetAllAsync(int? categoryId = null);

	Task<SubCategory?> GetByIdAsync(int id);
}
