using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

public record CommandUpdateDto(
    [Required]
    [MaxLength(250)]
    string HowTo,
    
    [Required]
    string CommandLine);