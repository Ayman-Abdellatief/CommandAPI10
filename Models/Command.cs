using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandAPI.Models;

public class Command : ICreatedAtTrackable
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public required string HowTo { get; set; }

    [Required]
    public required string CommandLine {get; set;}

    [Required]
    public DateTime CreatedAt { get; set; }

    // Foreign key to Platform
    public int PlatformId { get; set; }

    // Navigation property to represent the platform of the command
    [ForeignKey("PlatformId")]
    public Platform? Platform { get; set; }
}