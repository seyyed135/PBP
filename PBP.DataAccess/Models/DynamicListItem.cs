using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class DynamicListItem
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}