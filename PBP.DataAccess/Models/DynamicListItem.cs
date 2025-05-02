using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBP.DataAccess.Models;

public class DynamicListItem
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "int")]
    public CategoryName Category { get; set; }

    [Required]
    public string Value { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

public enum CategoryName
{
    [Display(Name = "گروه خونی")]
    BloodType = 1,

    [Display(Name = "محل تولد")]
    PlaceOfBirth = 2,

    [Display(Name = "نوع شبکه اجتماعی")]
    TypeOfSocialNetwork = 3,

    [Display(Name = "گروه سنی")]
    AgeGroup = 4,
}