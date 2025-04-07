using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class LoginViewModel
{
    [Required]
    [StringLength(100, ErrorMessage = "نام کاربری باید حداقل 3 کارکتر و حداکثر 100 کارکتر باشد.", MinimumLength = 3)]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "مرا به خاطر بسپار")]
    public bool RememberMe { get; set; }
}