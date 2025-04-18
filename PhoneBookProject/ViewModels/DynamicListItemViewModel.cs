using PBP.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class DynamicListItemViewModel
{
    [Required(ErrorMessage = "لطفا عنوان دسته بندی را انتخاب کنید")]
    public CategoryName Category { get; set; }

    [Required(ErrorMessage = "لطفا محتوا را وارد کنید")]
    [MaxLength(100, ErrorMessage = "محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    public string Value { get; set; } = string.Empty;

    internal void UpdateModel(DynamicListItem model)
    {
        model.Category = Category;
        model.Value = Value.Trim();
        model.IsActive = true;
    }
}