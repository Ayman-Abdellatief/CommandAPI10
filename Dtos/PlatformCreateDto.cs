using System.ComponentModel.DataAnnotations;


namespace CommandAPI.Dtos;
public record PlatformCreateDto(
    [Required]
    [MinLength(2)]
    string PlatformName);