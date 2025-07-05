using BulletinBoard.Application.Announcements;
using BulletinBoard.Infrastructure.Persistence.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "BulletinBoard API",
		Version = "v1",
		Description = "API for managing announcements",
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "BulletinBoard API v1");
	});
}

app.UseHttpsRedirection();
app.MapControllers();
app.UsePathBase("/api");

app.Run();
