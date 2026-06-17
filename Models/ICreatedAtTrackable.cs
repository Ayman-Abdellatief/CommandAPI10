namespace CommandAPI.Models;

/// <summary>
/// Interface for entities that track creation time automatically.
/// </summary>
public interface ICreatedAtTrackable
{
    DateTime CreatedAt { get; set; }
}