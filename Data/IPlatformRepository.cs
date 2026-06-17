using CommandAPI.Models;

namespace CommandAPI.Data;

public interface IPlatformRepository
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Platform>> GetPlatformsAsync();
    Task<Platform?> GetPlatformByIdAsync(int id);
    Task CreatePlatformAsync(Platform platform);
    Task UpdatePlatformAsync(Platform platform);
    void DeletePlatform(Platform platform);
}