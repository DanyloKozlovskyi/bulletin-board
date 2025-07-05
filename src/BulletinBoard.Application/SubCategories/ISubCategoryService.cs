using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Application.SubCategories;
public interface ISubCategoryService
{
	Task<IEnumerable<SubCategory>> ListAsync();
	Task<SubCategory?> GetByIdAsync(int id);
}
