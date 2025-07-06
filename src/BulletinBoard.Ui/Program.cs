using BulletinBoard.Ui.Features.Announcements;
using BulletinBoard.Ui.Features.Announcements.Adapters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("BulletinBoardApi", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

builder.Services.AddHttpClient<IAnnouncementAdapter, AnnouncementHttpAdapter>(c =>
{
	c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

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
