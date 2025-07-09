using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.SubCategories;
public interface ISubCategoryService
{
	Task<IEnumerable<SubCategory>> ListAsync(int? categoryId = null);
	Task<SubCategory?> GetByIdAsync(int id);
}
