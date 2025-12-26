using Microsoft.AspNetCore.Mvc;
using SmartHome.Common;
using SmartHome.Infrastructure.Models;
using SmartHome.REST.Models;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly ICrudServiceAsync<RoomModel> _service;

    public RoomsController(ICrudServiceAsync<RoomModel> service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.ReadAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.ReadAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDto dto)
    {
        var model = new RoomModel { Name = dto.Name };
        await _service.CreateAsync(model);
        // Повертаємо 201 Created згідно з правилами REST
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.ReadAsync(id);
        if (item == null) return NotFound();
        await _service.RemoveAsync(item);
        return NoContent(); // 204 No Content
    }
}