using System.ComponentModel.DataAnnotations;

namespace FileVault.Entities;

public class HasTimestamps
{
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    public DateTimeOffset UpdatedAt { get; set; }
}