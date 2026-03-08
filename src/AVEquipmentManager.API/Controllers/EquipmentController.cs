using AVEquipmentManager.API.Data;
using AVEquipmentManager.Shared.DTOs;
using AVEquipmentManager.Shared.Enums;
using AVEquipmentManager.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AVEquipmentManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipmentController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/equipment?room=Room 1&status=Active
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll(
        [FromQuery] string? room,
        [FromQuery] string? status)
    {
        var query = _context.Equipment.AsQueryable();

        if (!string.IsNullOrWhiteSpace(room))
            query = query.Where(e => e.RoomName.ToLower() == room.ToLower());

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<EquipmentStatus>(status, true, out var parsedStatus))
            query = query.Where(e => e.Status == parsedStatus);

        var items = await query.OrderBy(e => e.RoomName).ThenBy(e => e.Name).ToListAsync();
        return Ok(items.Select(MapToDto));
    }

    // GET /api/equipment/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EquipmentDto>> GetById(int id)
    {
        var item = await _context.Equipment.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(MapToDto(item));
    }

    // GET /api/equipment/rooms
    [HttpGet("rooms")]
    public async Task<ActionResult<IEnumerable<string>>> GetRooms()
    {
        var rooms = await _context.Equipment
            .Select(e => e.RoomName)
            .Distinct()
            .OrderBy(r => r)
            .ToListAsync();
        return Ok(rooms);
    }

    // POST /api/equipment
    [HttpPost]
    public async Task<ActionResult<EquipmentDto>> Create([FromBody] CreateEquipmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check for unique serial number
        if (await _context.Equipment.AnyAsync(e => e.SerialNumber == dto.SerialNumber))
            return Conflict(new { message = $"Serial number '{dto.SerialNumber}' already exists." });

        var equipment = new Equipment
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            RoomName = dto.RoomName,
            DateInstalled = dto.DateInstalled.ToUniversalTime(),
            ExpectedLifeInYears = dto.ExpectedLifeInYears,
            Status = dto.Status,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = equipment.Id }, MapToDto(equipment));
    }

    // PUT /api/equipment/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EquipmentDto>> Update(int id, [FromBody] UpdateEquipmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var equipment = await _context.Equipment.FindAsync(id);
        if (equipment == null) return NotFound();

        // Check for unique serial number (excluding the current item)
        if (await _context.Equipment.AnyAsync(e => e.SerialNumber == dto.SerialNumber && e.Id != id))
            return Conflict(new { message = $"Serial number '{dto.SerialNumber}' already exists." });

        equipment.Name = dto.Name;
        equipment.SerialNumber = dto.SerialNumber;
        equipment.RoomName = dto.RoomName;
        equipment.DateInstalled = dto.DateInstalled.ToUniversalTime();
        equipment.ExpectedLifeInYears = dto.ExpectedLifeInYears;
        equipment.Status = dto.Status;
        equipment.Notes = dto.Notes;
        equipment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(MapToDto(equipment));
    }

    // DELETE /api/equipment/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);
        if (equipment == null) return NotFound();

        if (equipment.Status != EquipmentStatus.Retired && equipment.Status != EquipmentStatus.Decommissioned)
            return BadRequest(new { message = "Equipment can only be deleted if its status is Retired or Decommissioned." });

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static EquipmentDto MapToDto(Equipment e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        SerialNumber = e.SerialNumber,
        RoomName = e.RoomName,
        DateInstalled = e.DateInstalled,
        ExpectedLifeInYears = e.ExpectedLifeInYears,
        Status = e.Status,
        Notes = e.Notes,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };
}
