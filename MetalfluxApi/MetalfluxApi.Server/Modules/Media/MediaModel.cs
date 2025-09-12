using System.ComponentModel.DataAnnotations;

namespace MetalfluxApi.Server.Modules.Media;

public class MediaModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Url { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
