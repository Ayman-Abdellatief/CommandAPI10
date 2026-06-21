using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 using CommandAPI.Dtos;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepo;
    private readonly ICommandRepository _commandRepo;
   public PlatformsController(
    IPlatformRepository platformRepo, 
    ICommandRepository commandRepo)
{
    _platformRepo = platformRepo;
    _commandRepo = commandRepo;
}

    [HttpGet]
[HttpGet]
public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
{
    var platforms = await _platformRepo.GetPlatformsAsync();

    // Manual mapping to DTOs
    var platformDtos = platforms.Select(p => new PlatformReadDto(p.Id, p.PlatformName, p.CreatedAt));

    return Ok(platformDtos);
}

[HttpGet("{id}", Name = "GetPlatformById")]
public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
{
    var platform = await _platformRepo.GetPlatformByIdAsync(id);
    if (platform == null)
        return NotFound();

    // Manual mapping to DTO
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

    await _platformRepo.CreatePlatformAsync(platform);
    await _platformRepo.SaveChangesAsync();

    // Manual mapping to DTO for response
    var platformReadDto = new PlatformReadDto(platform.Id, platform.PlatformName, platform.CreatedAt);

    return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platformReadDto);
}

[HttpPut("{id}")]
public async Task<ActionResult> UpdatePlatform(int id, PlatformUpdateDto platformUpdateDto)
{
    var platformFromRepo = await _platformRepo.GetPlatformByIdAsync(id);
    if (platformFromRepo == null)
    {
        return NotFound();
    }

    // Manual mapping from DTO to entity
    platformFromRepo.PlatformName = platformUpdateDto.PlatformName;

    await _platformRepo.UpdatePlatformAsync(platformFromRepo);
    await _platformRepo.SaveChangesAsync();

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<ActionResult> DeletePlatform(int id)
{
    var platformFromRepo = await _platformRepo.GetPlatformByIdAsync(id);
    if (platformFromRepo == null)
    {
        return NotFound();
    }

    _platformRepo.DeletePlatform(platformFromRepo);
    await _platformRepo.SaveChangesAsync();

    return NoContent();
}
[HttpGet("{platformId}/commands")]
public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {

    var platform = await _platformRepo.GetPlatformByIdAsync(platformId);
    if (platform == null)
        return NotFound();

    var commands = await _commandRepo.GetCommandsByPlatformIdAsync(platformId);
    var commandDtos = commands.Select(c => new CommandReadDto(c.Id, c.HowTo, c.CommandLine, c.PlatformId, c.CreatedAt));

    return Ok(commandDtos);
}

}