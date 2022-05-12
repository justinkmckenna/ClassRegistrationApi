using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationApi.Models;

public record RegistrationRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
    [Required]
    public DateTime? DateOfCourse { get; init; }
    [Required, MaxLength(500)]
    public string Course { get; init; } = string.Empty;
};
public record Registration(string id, RegistrationRequest registration);
