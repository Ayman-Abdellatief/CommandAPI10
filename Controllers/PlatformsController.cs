using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 using CommandAPI.Dtos;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlatformsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
{
  var platforms = await _context.Platforms.ToListAsync();

 
   // Manual mapping to DTOs
var platformDtos = platforms.Select(p => new PlatformReadDto(p.Id, p.PlatformName, p.CreatedAt));

  return Ok(platformDtos);
}

[HttpGet("{id}", Name = "GetPlatformById")]
public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
{
  var platform = await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
  if (platform == null)
    return NotFound();

  // Manual mapping to DTO
 // Manual mapping to DTO for response
var platformDto = new PlatformReadDto(platform.Id, platform.PlatformName, platform.CreatedAt);

  return Ok(platformDto);
}

[HttpPost]
public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
{
  // Manual mapping from DTO to entity
  var platform = new Platform
  {
    PlatformName = platformCreateDto.PlatformName
  };

  await _context.Platforms.AddAsync(platform);
  await _context.SaveChangesAsync();

  // Manual mapping to DTO for response
 var platformReadDto = new PlatformReadDto(platform.Id, platform.PlatformName, platform.CreatedAt);

  return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platformReadDto);
}

[HttpPut("{id}")]
public async Task<ActionResult> UpdatePlatform(int id, PlatformUpdateDto platformUpdateDto)
{
  var platformFromContext = await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
  if (platformFromContext == null)
  {
    return NotFound();
  }

  // Manual mapping from DTO to entity
  platformFromContext.PlatformName = platformUpdateDto.PlatformName;

  await _context.SaveChangesAsync();

  return NoContent();
}

[HttpDelete("{id}")]
public async Task<ActionResult> DeletePlatform(int id)
{
    var platformFromContext = await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
    if (platformFromContext == null)
    {
        return NotFound();
    }
    _context.Platforms.Remove(platformFromContext);
    await _context.SaveChangesAsync();

    return NoContent();
}
}