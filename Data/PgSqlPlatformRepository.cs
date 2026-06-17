using Microsoft.EntityFrameworkCore;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class PgSqlPlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _context;

    public PgSqlPlatformRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public async Task<IEnumerable<Platform>> GetPlatformsAsync()
    {
        var platforms = await _context.Platforms.AsNoTracking().ToListAsync();

        return platforms;
    }

    public async Task<Platform?> GetPlatformByIdAsync(int id)
    {
        return await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        await _context.Platforms.AddAsync(platform);
    }    

    public Task UpdatePlatformAsync(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        return Task.CompletedTask;
    }

    public void DeletePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        _context.Platforms.Remove(platform);
    }
}