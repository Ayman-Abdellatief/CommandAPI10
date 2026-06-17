using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

public record CommandCreateDto(
    [Required]
    [MaxLength(250)]
    string HowTo,
    
    [Required]
    string CommandLine,
    
    [Required]
    int PlatformId);