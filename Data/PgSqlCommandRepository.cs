using Microsoft.EntityFrameworkCore;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class PgSqlCommandRepository : ICommandRepository
{
    private readonly AppDbContext _context;

    public PgSqlCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public async Task<IEnumerable<Command>> GetCommandsAsync()
    {
        var commands = await _context.Commands.AsNoTracking().ToListAsync();

        return commands;
    }

    public async Task<Command?> GetCommandByIdAsync(int id)
    {
        return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateCommandAsync(Command command)
    {
        if(command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        await _context.Commands.AddAsync(command);
    }

    public Task UpdateCommandAsync(Command command)
    {
        if(command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        return Task.CompletedTask;
    }

    public void DeleteCommand(Command command)
    {
        if(command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        _context.Commands.Remove(command);
    }


    public async Task<IEnumerable<Command>> GetCommandsByPlatformIdAsync(int platformId)
    {
        var commands = await _context.Commands
            .AsNoTracking()
            .Where(c => c.PlatformId == platformId)
            .ToListAsync();

        return commands;
    }   
}