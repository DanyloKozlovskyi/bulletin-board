using BulletinBoard.Ui.Features.Announcements;
using BulletinBoard.Ui.Features.Announcements.Adapters;
using BulletinBoard.Ui.Features.Categories;
using BulletinBoard.Ui.Features.SubCategories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IAnnouncementAdapter, AnnouncementHttpAdapter>(c =>
{
	c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddHttpClient<ICategoryAdapter, CategoryHttpAdapter>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddHttpClient<ISubCategoryAdapter, SubCategoryHttpAdapter>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Announcement}/{action=Index}")
	.WithStaticAssets();


app.Run();
