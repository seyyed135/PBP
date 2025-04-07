using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class RegisterViewModel
{
    [Required]
    [StringLength(100, ErrorMessage = "نام کاربری باید حداقل 3 کارکتر و حداکثر 100 کارکتر باشد.", MinimumLength = 3)]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; } = string.Empty;   

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;   

    [DataType(DataType.Password)]
    [Display(Name = "تایید رمز عبور")]
    [Compare("Password", ErrorMessage = "رمز عبور و رمز عبور تأیید مطابقت ندارند.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}