using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Mapster;
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
   var commandDtos = commands.Select(c => c.Adapt<CommandReadDto>());

    return Ok(commandDtos);
}

[HttpGet("{id}", Name = "GetCommandById")]
public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
{
    var command = await _commandRepo.GetCommandByIdAsync(id);
    if (command == null)
        return NotFound();

    // Manual mapping to DTO
var commandDto = command.Adapt<CommandReadDto>();
    return Ok(commandDto);
}

[HttpPost]
public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
{
    // Manual mapping from DTO to entity
    // var command = new Command
    // {
    //     HowTo = commandCreateDto.HowTo,
    //     CommandLine = commandCreateDto.CommandLine,
    //     PlatformId = commandCreateDto.PlatformId
    // };

var command = commandCreateDto.Adapt<Command>();

    await _commandRepo.CreateCommandAsync(command);
    await _commandRepo.SaveChangesAsync();

  // Using Mapster
var commandReadDto = command.Adapt<CommandReadDto>();

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

    // // Manual mapping from DTO to entity
    // commandFromRepo.HowTo = commandUpdateDto.HowTo;
    // commandFromRepo.CommandLine = commandUpdateDto.CommandLine;

    // Using Mapster
        commandUpdateDto.Adapt(commandFromRepo);

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