using PBP.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class SearchChangesHistoryViewModel
{
    public FieldName? FieldName { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? StartDate { get; set; }

    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? EndDate { get; set; }

    public List<ContactChangeHistory>? ContactChangeHistorys { get; set; }
}