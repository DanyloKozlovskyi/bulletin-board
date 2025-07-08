using BulletinBoard.Application.Categories;
using BulletinBoard.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BulletinBoard.Infrastructure.Persistence.Repositories;
public class CategoryRepository : ICategoryRepository
{
	private readonly IDbConnection _db;

	public CategoryRepository(IConfiguration config)
		=> _db = new SqlConnection(config.GetConnectionString("AzureConnection"));

	public Task<IEnumerable<Category>> ListAsync()
		=> _db.QueryAsync<Category>(
			 "dbo.sp_ListCategories",
			 commandType: CommandType.StoredProcedure);

	public Task<Category?> GetByIdAsync(int id)
	{
		return _db.QuerySingleOrDefaultAsync<Category>(
			 "dbo.sp_GetCategoryById",
			 new
			 {
				 Id = id
			 },
			 commandType: CommandType.StoredProcedure);
	}
}
