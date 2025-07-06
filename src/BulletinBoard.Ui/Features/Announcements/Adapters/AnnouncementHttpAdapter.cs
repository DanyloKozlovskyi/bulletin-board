using BulletinBoard.Ui.Features.Announcements.Models;

namespace BulletinBoard.Ui.Features.Announcements.Adapters;
public class AnnouncementHttpAdapter : IAnnouncementAdapter
{
	private readonly HttpClient _client;
	public AnnouncementHttpAdapter(HttpClient client)
	{
		_client = client;
	}
	public async Task<List<Announcement>> GetAllAsync(int? categoryId = null, int? subCategoryId = null)
	{
		var query = new List<string>();
		if (categoryId.HasValue) query.Add($"categoryId={categoryId}");
		if (subCategoryId.HasValue) query.Add($"subCategoryId={subCategoryId}");
		var url = "announcements" + (query.Any() ? "?" + string.Join("&", query) : "");
		return await _client.GetFromJsonAsync<List<Announcement>>(url)
			   ?? new List<Announcement>();
	}

	public async Task<Announcement?> GetByIdAsync(Guid id)
	{
		return await _client.GetFromJsonAsync<Announcement>($"announcements/{id}");
	}

	public async Task<Guid> CreateAsync(AnnouncementCreateModel model)
	{
		var resp = await _client.PostAsJsonAsync("announcements", model);
		resp.EnsureSuccessStatusCode();
		return await resp.Content.ReadFromJsonAsync<Guid>()!;
	}

	public async Task UpdateAsync(AnnouncementUpdateModel model)
	{
		var resp = await _client.PutAsJsonAsync($"announcements/{model.Id}", model);
		resp.EnsureSuccessStatusCode();
	}

	public async Task DeleteAsync(Guid id)
	{
		var resp = await _client.DeleteAsync($"announcements/{id}");
		resp.EnsureSuccessStatusCode();
	}
}