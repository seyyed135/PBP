using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class DynamicListItem
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public CategoryName Category { get; set; } 

    [Required]
    public string Value { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

public enum CategoryName
{
    [Display(Name = "گروه خونی")]
    BloodType,

    [Display(Name = "محل تولد")]
    PlaceOfBirth,

    [Display(Name = "نوع شبکه اجتماعی")]
    TypeOfSocialNetwork,

    [Display(Name = "گروه سنی")]
    AgeGroup,
}