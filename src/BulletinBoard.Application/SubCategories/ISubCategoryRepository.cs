using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.SubCategories;
public interface ISubCategoryRepository
{
	Task<IEnumerable<SubCategory>> ListAsync();
	Task<SubCategory?> GetByIdAsync(int id);
	Task<IEnumerable<SubCategory>> ListByCategoryAsync(int categoryId);
}