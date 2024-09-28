using System.ComponentModel.DataAnnotations;

namespace FileVault.Entities;

public class File
{
    public int Id { get; set; }

    [MaxLength(65535)]
    public string Name { get; set; }
    
    // [MaxLength(65535)]
    // public string Url {get; set; }

    // Can be null if the object is uploaded via the API
    public string? UserId { get; set; }
    public User? User { get; set; }
}