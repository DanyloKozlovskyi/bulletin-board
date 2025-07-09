namespace BulletinBoard.Ui.Features.Categories;
public class CategoryHttpAdapter : ICategoryAdapter
{
	private readonly HttpClient _http;
	public CategoryHttpAdapter(HttpClient http)
	{
		_http = http;
	}

	public async Task<IEnumerable<Category>> GetAllAsync()
	{
		return await _http
			.GetFromJsonAsync<List<Category>>("categories")
			?? new List<Category>();
	}

	public async Task<Category?> GetByIdAsync(int id)
	{
		return await _http
			.GetFromJsonAsync<Category>($"categories/{id}");
	}
}