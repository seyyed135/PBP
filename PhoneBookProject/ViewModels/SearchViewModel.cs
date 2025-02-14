using PBP.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class SearchViewModel
{
    public string? SearchName { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره همراه باید ۱۱ رقمی باشد.")]
    public string? SearchPhone { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? StartDate { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? EndDate { get; set; }

    public List<Contact>? Contacts { get; set; }
}