using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepository _commandRepo;

    public CommandsController(ICommandRepository commandRepo)
    {
        _commandRepo = commandRepo;
    }


    [HttpGet]
public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommands()
{
    var commands = await _commandRepo.GetCommandsAsync();

    // Manual mapping to DTOs
    var commandDtos = commands.Select(c => new CommandReadDto(c.Id, c.HowTo, c.CommandLine, c.PlatformId, c.CreatedAt));

    return Ok(commandDtos);
}

[HttpGet("{id}", Name = "GetCommandById")]
public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
{
    var command = await _commandRepo.GetCommandByIdAsync(id);
    if (command == null)
        return NotFound();

    // Manual mapping to DTO
    var commandDto = new CommandReadDto(command.Id, command.HowTo, command.CommandLine, command.PlatformId, command.CreatedAt);

    return Ok(commandDto);
}

[HttpPost]
public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
{
    // Manual mapping from DTO to entity
    var command = new Command
    {
        HowTo = commandCreateDto.HowTo,
        CommandLine = commandCreateDto.CommandLine,
        PlatformId = commandCreateDto.PlatformId
    };

    await _commandRepo.CreateCommandAsync(command);
    await _commandRepo.SaveChangesAsync();

    // Manual mapping to DTO for response
    var commandReadDto = new CommandReadDto(command.Id, command.HowTo, command.CommandLine, command.PlatformId, command.CreatedAt);

    return CreatedAtRoute(nameof(GetCommandById), new { Id = command.Id }, commandReadDto);
}
[HttpPut("{id}")]
public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
{
    var commandFromRepo = await _commandRepo.GetCommandByIdAsync(id);
    if (commandFromRepo == null)
    {
        return NotFound();
    }

    // Manual mapping from DTO to entity
    commandFromRepo.HowTo = commandUpdateDto.HowTo;
    commandFromRepo.CommandLine = commandUpdateDto.CommandLine;

    await _commandRepo.UpdateCommandAsync(commandFromRepo);
    await _commandRepo.SaveChangesAsync();

    return NoContent();
}
[HttpDelete("{id}")]
public async Task<ActionResult> DeleteCommand(int id)
{
    var commandFromRepo = await _commandRepo.GetCommandByIdAsync(id);
    if (commandFromRepo == null)
    {
        return NotFound();
    }

    _commandRepo.DeleteCommand(commandFromRepo);
    await _commandRepo.SaveChangesAsync();

    return NoContent();
}
}