using BulletinBoard.Application.Announcements;
using BulletinBoard.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BulletinBoard.Infrastructure.Persistence.Repositories;
public class AnnouncementRepository : IAnnouncementRepository
{
	private readonly IDbConnection _db;

	public AnnouncementRepository(IConfiguration config)
	{
		_db = new SqlConnection(config.GetConnectionString("Default"));
	}

	public async Task<IEnumerable<Announcement>> ListAsync()
	{
		return await _db.QueryAsync<Announcement>(
			"dbo.sp_ListAnnouncements",
			commandType: CommandType.StoredProcedure);
	}

	public async Task<Announcement> GetByIdAsync(Guid id)
	{
		return await _db.QuerySingleOrDefaultAsync<Announcement>(
			"dbo.sp_GetAnnouncementById",
			new { Id = id },
			commandType: CommandType.StoredProcedure);
	}

	public async Task<Guid> AddAsync(Announcement entity)
	{
		var newId = await _db.ExecuteScalarAsync<Guid>(
			"dbo.sp_CreateAnnouncement",
			new
			{
				entity.Title,
				entity.Description,
				entity.Status,
				entity.Category,
				entity.SubCategory
			},
			commandType: CommandType.StoredProcedure);
		return newId;
	}

	public async Task UpdateAsync(Announcement entity)
	{
		await _db.ExecuteAsync(
			"dbo.sp_UpdateAnnouncement",
			new
			{
				entity.Id,
				entity.Title,
				entity.Description,
				entity.Status,
				entity.Category,
				entity.SubCategory
			},
			commandType: CommandType.StoredProcedure);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _db.ExecuteAsync(
			"dbo.sp_DeleteAnnouncement",
			new { Id = id },
			commandType: CommandType.StoredProcedure);
	}
}
