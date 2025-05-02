using System.ComponentModel.DataAnnotations;

namespace PBP.DataAccess.Models;

public class Contact
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    public int? BloodTypeId { get; set; }
    public DynamicListItem? BloodType { get; set; }

    public int? PlaceOfBirthId { get; set; }
    public DynamicListItem? PlaceOfBirth { get; set; }

    public int? TypeOfSocialNetworkId { get; set; }
    public DynamicListItem? TypeOfSocialNetwork { get; set; }

    public int? AgeGroupId { get; set; }
    public DynamicListItem? AgeGroup { get; set; }

    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public ICollection<ContactChangeHistory>? ChangesHistory { get; set; }
}