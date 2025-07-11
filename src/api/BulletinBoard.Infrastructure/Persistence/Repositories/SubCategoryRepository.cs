﻿using BulletinBoard.Application.SubCategories;
using BulletinBoard.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BulletinBoard.Infrastructure.Persistence.Repositories;
public class SubCategoryRepository : ISubCategoryRepository
{
	private readonly IDbConnection _db;

	public SubCategoryRepository(IConfiguration config)
	{
		_db = new SqlConnection(config.GetConnectionString("AzureConnection"));
	}

	public Task<IEnumerable<SubCategory>> ListAsync()
	{
		return _db.QueryAsync<SubCategory>(
			"dbo.sp_ListSubCategories",
			 commandType: CommandType.StoredProcedure);
	}

	public Task<SubCategory?> GetByIdAsync(int id)
	{
		return _db.QuerySingleOrDefaultAsync<SubCategory>(
			 "dbo.sp_GetSubCategoryById",
			 new { Id = id },
			 commandType: CommandType.StoredProcedure);
	}
	public Task<IEnumerable<SubCategory>> ListByCategoryAsync(int categoryId)
	{
		return _db.QueryAsync<SubCategory>(
			 "dbo.sp_ListSubCategoriesByCategory",
			 new { CategoryId = categoryId },
			 commandType: CommandType.StoredProcedure);
	}
}
