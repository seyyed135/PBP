using PBP.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class SearchContactViewModel
{
    public string? SearchName { get; set; }

    [RegularExpression(@"^09\d{1,9}$", ErrorMessage = "شماره همراه باید با 09 شروع شده و بین 3 تا 11 رقم باشد.")]
    public string? SearchPhone { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? StartDate { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? EndDate { get; set; }

    public List<Contact>? Contacts { get; set; }
}