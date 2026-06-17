using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models;

public class Platform : ICreatedAtTrackable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string PlatformName { get; set; }

    
    [Required]
    public DateTime CreatedAt { get; set; }

     public ICollection<Command> Commands { get; set; } = new List<Command>();
}