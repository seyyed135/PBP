using PBP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PBP.ViewModels;

public class ContactViewModel
{
    public ContactViewModel() { }

    public ContactViewModel(Contact contact)
    {
        Id = contact.Id;
        Name = contact.Name;
        PhoneNumber = contact.PhoneNumber;
        BirthDate = GregorianDateToPersianString(contact.BirthDate);
        Image = ConvertToIFormFile(contact.Image?.Data ?? Array.Empty<byte>());
    }

    public int? Id { get; set; }

    public bool IsEdit => Id > 0;

    [Required(ErrorMessage = "لطفا نام را وارد کنید")]
    [MaxLength(100)]
    [DisplayName("نام")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید")]
    [DisplayName("شماره تلفن")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره همراه باید ۱۱ رقمی باشد.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [DisplayName("تاریخ تولد")]
    [RegularExpression(@"^([۰-۹]{4})/([۰-۹]{2})/([۰-۹]{2})$", ErrorMessage = "تاریخ تولد باید به صورت شمسی و فرمت YYYY/MM/DD باشد.")]
    public string? BirthDate { get; set; }

    [DisplayName("تصویر")]
    public IFormFile? Image { get; set; }

    internal async Task UpdateModelAsync(Contact model)
    {
        model.Name = Name.Trim();
        model.PhoneNumber = PhoneNumber.Trim();
        model.BirthDate = PersianStringToGregorianDate(BirthDate);

        try
        {
            if (Image != null && Image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Image.CopyToAsync(ms);
                    model.Image = new Image { Data = ms.ToArray() };
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(" ذخیره‌سازی تصویر با مشکل مواجه شد : ", ex);
        }
    }

    IFormFile ConvertToIFormFile(byte[] fileBytes)
    {
        var stream = new MemoryStream(fileBytes);
        return new FormFile(stream, 0, fileBytes.Length, "ImageFile", "default.jpg")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
    }

    DateTime? PersianStringToGregorianDate(string? persianDate)
    {
        if (string.IsNullOrEmpty(persianDate))
            return null;
        try
        {
            persianDate = ConvertPersianToEnglishNumbers(persianDate);
            var parts = persianDate.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            var pc = new PersianCalendar();

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0); ;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("تاریخ شمسی معتبر نیست", ex);
        }
    }

    string? GregorianDateToPersianString(DateTime? gregorianDate)
    {
        if (gregorianDate is null)
        {
            return null;
        }
        else
        {
            var pc = new PersianCalendar();
            int year = pc.GetYear(gregorianDate!.Value);
            int month = pc.GetMonth(gregorianDate!.Value);
            int day = pc.GetDayOfMonth(gregorianDate!.Value);

            return $"{ConvertEnglishToPersianNumbers(year.ToString("D4"))}/{ConvertEnglishToPersianNumbers(month.ToString("D2"))}/{ConvertEnglishToPersianNumbers(day.ToString("D2"))}";
        }
    }

    string ConvertPersianToEnglishNumbers(string persianString)
    {
        var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
        var englishDigits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        for (int i = 0; i < persianDigits.Length; i++)
        {
            persianString = persianString.Replace(persianDigits[i], englishDigits[i]);
        }
        return persianString;
    }

    string ConvertEnglishToPersianNumbers(string numberString)
    {
        var englishDigits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };

        for (int i = 0; i < englishDigits.Length; i++)
        {
            numberString = numberString.Replace(englishDigits[i], persianDigits[i]);
        }
        return numberString;
    }
}