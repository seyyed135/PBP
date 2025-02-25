using PBP.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class SearchChangesHistoryViewModel
{
    public FieldName? FieldName { get; set; }

    public string? Content { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? StartDate { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? EndDate { get; set; }

    [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "ساعت باید فرمت HH:mm باشد ، برای مثال 14:35")]
    public string? StartTime { get; set; }

    [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "ساعت باید فرمت HH:mm باشد ، برای مثال 14:35")]
    public string? EndTime { get; set; }

    public List<ContactChangeHistory>? ContactChangeHistorys { get; set; }
}