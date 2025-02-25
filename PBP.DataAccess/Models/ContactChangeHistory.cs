using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class ContactChangeHistory
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;

    [Required]
    public FieldName FieldName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public byte[]? OldImage { get; set; }

    public byte[]? NewImage { get; set; }

    [Required]
    public DateTime ChangedDate { get; set; }

    [Required]
    public string ChangedTime { get; set; } = string.Empty;
}

public enum FieldName
{
    [Display(Name = "نام")]
    Name,

    [Display(Name = "شماره تلفن")]
    PhoneNumber,

    [Display(Name = "تاریخ تولد")]
    BirthDate,

    [Display(Name = "تصویر")]
    Image,
}