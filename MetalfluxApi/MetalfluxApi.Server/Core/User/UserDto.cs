using System.ComponentModel.DataAnnotations;

namespace MetalfluxApi.Server.Core.User;

public class UserDto
{
    public int? Id { get; set; }

    [Required, StringLength(30)]
    public string Username { get; set; } = string.Empty;

    [Required, StringLength(255), EmailAddress]
    public string Email { get; set; } = string.Empty;

    public int? MentorId { get; set; } = null;

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
