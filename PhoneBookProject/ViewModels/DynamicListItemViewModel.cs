using PBP.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PBP.ViewModels;

public class DynamicListItemViewModel
{
    [Required(ErrorMessage = "لطفا عنوان دسته بندی را وارد کنید")]
    [MaxLength(20, ErrorMessage = "عنوان دسته بندی نمی‌تواند بیشتر از 20 کاراکتر باشد")]
    [MinLength(3, ErrorMessage = "عنوان دسته بندی باید حداقل ۳ کاراکتر باشد")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "لطفا محتوا را وارد کنید")]
    [MaxLength(100, ErrorMessage = "محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    public string Value { get; set; } = string.Empty;

    internal void UpdateModel(DynamicListItem model)
    {
        model.Category = Category.Trim();
        model.Value = Value.Trim();
        model.IsActive = true;
    }
}