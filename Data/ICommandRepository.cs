using CommandAPI.Models;

namespace CommandAPI.Data;

public interface ICommandRepository
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Command>> GetCommandsAsync();
    Task<Command?> GetCommandByIdAsync(int id);
    Task<IEnumerable<Command>> GetCommandsByPlatformIdAsync(int platformId);
    Task CreateCommandAsync(Command command);
    Task UpdateCommandAsync(Command command);
    void DeleteCommand(Command command);
}