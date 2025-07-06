namespace BulletinBoard.Ui.Features.Categories;
public interface ICategoryAdapter
{
	Task<IEnumerable<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
}
