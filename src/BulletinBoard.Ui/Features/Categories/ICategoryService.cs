namespace BulletinBoard.Ui.Features.Categories;
public interface ICategoryService
{
	Task<IEnumerable<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
}
