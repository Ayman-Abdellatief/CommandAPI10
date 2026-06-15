using System.ComponentModel.DataAnnotations;

using CommandAPI.Dtos;
public record PlatformUpdateDto(
    [Required]
    [MinLength(2)]
    string PlatformName);