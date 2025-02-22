using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Models;
using PBP.DataAccess.Repository;
using PBP.ViewModels;

namespace PBP.Controllers;

public class ContactsController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region See Contacts

    [HttpGet]
    public async Task<IActionResult> Index(SearchViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var contactViewModel = new ContactViewModel();
            var gregorianStartDate = contactViewModel.PersianStringToGregorianDate(viewModel.StartDate);
            var gregorianEndDate = contactViewModel.PersianStringToGregorianDate(viewModel.EndDate);

            var query = _unitOfWork.ContactRepository.GetFilteredContactsWithImages(
                                                        viewModel.SearchName,
                                                        viewModel.SearchPhone,
                                                        gregorianStartDate,
                                                        gregorianEndDate
                                                    );

            viewModel.Contacts = await query.ToListAsync();

            if (viewModel.Contacts.Count > 0)
                return View(viewModel);

            else
            {
                ModelState.AddModelError(string.Empty, "مخاطبی با مشخصات وارد شده یافت نشد");
                return View(viewModel);
            }
        }
        return View(viewModel);
    }

    #endregion

    #region Create/Edit Contact

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        var viewModel = new ContactViewModel();

        if (id > 0)
        {
            var contact = await _unitOfWork.ContactRepository.GetByIdAsync(id!.Value);

            if (contact == null)
                ModelState.AddModelError(string.Empty, $" مخاطب با شناسه {id} پیدا نشد .");
            else
                viewModel = new ContactViewModel(contact);
        }

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactViewModel viewModel)
    {
        var contact = new Contact();

        if (ModelState.IsValid)
        {
            if (viewModel.IsEdit)
            {
                contact = await _unitOfWork.ContactRepository.GetByIdAsync(viewModel.Id!.Value) ?? new();
            }

            if (await IsExistPhoneNumberAsync(viewModel.PhoneNumber, viewModel.Id))
            {
                ModelState.AddModelError("PhoneNumber", $"شماره تلفن {viewModel.PhoneNumber} از قبل در سیستم وجود دارد .");
                return View(viewModel);
            }

            if (!viewModel.PhoneNumber.Trim().StartsWith("09"))
            {
                ModelState.AddModelError("PhoneNumber", "لطفا از فرمت صحیح شماره تلفن استفاده کنید ، برای مثال : 09111111111");
                return View(viewModel);
            }

            if (viewModel.IsEdit && viewModel.Image != null)
            {
                var selectedContactFromDb = await _unitOfWork.ContactRepository.GetContactByIdWithImageAsync(viewModel.Id!.Value);

                if (selectedContactFromDb != null && selectedContactFromDb.Image != null)
                {
                    _unitOfWork.ContactRepository.DeleteImage(selectedContactFromDb.Image);
                }
            }

            await viewModel.UpdateModelAsync(contact);

            if (contact.BirthDate != null && contact.BirthDate > DateTime.UtcNow)
            {
                ModelState.AddModelError("BirthDate", "تاریخ تولد نمیتواند برای آینده باشد");
                return View(viewModel);
            }

            if (!viewModel.IsEdit)
            {
                await _unitOfWork.ContactRepository.AddAsync(contact);
                TempData["success"] = "مخاطب جدید با موفقیت ذخیره شد";
            }
            else
            {
                _unitOfWork.ContactRepository.Update(contact);
                TempData["success"] = "مخاطب با موفقیت ویرایش شد";
            }
            await _unitOfWork.ContactRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }

    #endregion

    #region Delete Contact

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var contact = await _unitOfWork.ContactRepository.GetContactByIdWithImageAsync(id!.Value);

        if (contact == null)
            return NotFound();

        return View(contact);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contact = await _unitOfWork.ContactRepository.GetContactByIdWithImageAsync(id);

        if (contact?.Image != null)
            _unitOfWork.ContactRepository.DeleteImage(contact.Image);

        if (contact != null)
            _unitOfWork.ContactRepository.Delete(contact);

        await _unitOfWork.ContactRepository.SaveAsync();
        TempData["success"] = "مخاطب با موفقیت حذف شد";
        return RedirectToAction(nameof(Index));
    }

    #endregion

    async Task<bool> IsExistPhoneNumberAsync(string phoneNumber, int? id)
        => await _unitOfWork.ContactRepository.AnyAsync(c =>
                                                        c.PhoneNumber == phoneNumber &&
                                                        (id == null || c.Id != id));
}