namespace BulletinBoard.Ui.Features.SubCategories;
public interface ISubCategoryService
{
	Task<IEnumerable<SubCategory>> GetAllAsync(int? categoryId = null);
	Task<SubCategory?> GetByIdAsync(int id);
}
