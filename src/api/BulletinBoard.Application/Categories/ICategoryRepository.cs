using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.Categories;
public interface ICategoryRepository
{
	Task<IEnumerable<Category>> ListAsync();
	Task<Category?> GetByIdAsync(int id);
}
