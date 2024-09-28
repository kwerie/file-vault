using Microsoft.AspNetCore.Identity;

namespace FileVault.Entities;

public class User : IdentityUser
{
    public ICollection<File> Files { get; set; } = [];
}