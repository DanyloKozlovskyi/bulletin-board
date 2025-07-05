using BulletinBoard.Application.Announcements;
using BulletinBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class AnnouncementsController : ControllerBase
{
	private readonly IAnnouncementService _service;

	public AnnouncementsController(IAnnouncementService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Announcement>>> GetAll([FromQuery] int? categoryId = null, [FromQuery] int? subCategoryId = null)
	{
		var list = await _service.ListAsync(categoryId, subCategoryId);
		return Ok(list);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<Announcement>> GetById(Guid id)
	{
		var announcement = await _service.GetByIdAsync(id);
		if (announcement is null)
			return NotFound();
		return Ok(announcement);
	}

	[HttpPost]
	public async Task<ActionResult> Create([FromBody] AnnouncementCreateModel model)
	{
		var newId = await _service.CreateAsync(model);
		return CreatedAtAction(nameof(GetById), new { id = newId }, null);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> Update(Guid id, [FromBody] AnnouncementUpdateModel model)
	{
		if (id != model.Id)
			return BadRequest("ID in URL and payload must match");

		var exists = await _service.GetByIdAsync(id);
		if (exists is null)
			return NotFound();

		await _service.UpdateAsync(model);
		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		var exists = await _service.GetByIdAsync(id);
		if (exists is null)
			return NotFound();

		await _service.DeleteAsync(id);
		return NoContent();
	}
}