namespace BulletinBoard.Ui.Features.SubCategories;
public class SubCategoryHttpAdapter : ISubCategoryAdapter
{
	private readonly HttpClient _client;
	public SubCategoryHttpAdapter(HttpClient client)
	{
		_client = client;
	}

	public async Task<List<SubCategory>> GetAllAsync(int? categoryId = null)
	{
		var url = "subcategories"
				+ (categoryId.HasValue
					? $"?categoryId={categoryId.Value}"
					: "");
		return await _client.GetFromJsonAsync<List<SubCategory>>(url)
			   ?? new List<SubCategory>();
	}

	public async Task<SubCategory?> GetByIdAsync(int id)
	{
		return await _client.GetFromJsonAsync<SubCategory>($"subcategories/{id}");
	}
}