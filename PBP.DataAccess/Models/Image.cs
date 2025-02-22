using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class Image
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public byte[] Data { get; set; } = null!;
}