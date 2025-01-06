using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.Data;
using PBP.Models;
using PBP.ViewModels;

namespace PBP.Controllers;

public class ContactsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    #region See Contacts

    public async Task<IActionResult> Index(string searchName, string searchPhone, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Set<Contact>()
                            .Include(c => c.Image)
                            .AsQueryable();

        if (!string.IsNullOrEmpty(searchName))
            query = query.Where(c => c.Name.Contains(searchName));

        if (!string.IsNullOrEmpty(searchPhone))
            query = query.Where(c => c.PhoneNumber.Contains(searchPhone));

        if (startDate.HasValue)
            query = query.Where(c => c.BirthDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(c => c.BirthDate <= endDate.Value);

        return View(await query.ToListAsync());
    }

    #endregion

    #region Create/Edit Contact

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        var viewModel = new ContactViewModel();

        if (id > 0)
        {
            var contact = await _context.Set<Contact>()
                                        .Include(c => c.Image)
                                        .SingleOrDefaultAsync(c => c.Id == id);
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
                contact = await _context.Set<Contact>().FindAsync(viewModel.Id) ?? new();
            }

            if (IsExistPhoneNumber(viewModel.PhoneNumber, viewModel.Id))
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
                var selectedContactFromDb = await _context.Set<Contact>()
                                                            .Include(c => c.Image)
                                                            .SingleOrDefaultAsync(c => c.Id == viewModel.Id);
                if (selectedContactFromDb != null && selectedContactFromDb.Image != null)
                {
                    _context.Remove(selectedContactFromDb.Image);
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
                _context.Add(contact);
                TempData["success"] = "مخاطب جدید با موفقیت ذخیره شد";
            }
            else
            {
                _context.Update(contact);
                TempData["success"] = "مخاطب با موفقیت ویرایش شد";
            }
            await _context.SaveChangesAsync();
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

        var contact = await _context.Set<Contact>()
                                    .Include(c => c.Image)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        if (contact == null)
            return NotFound();

        return View(contact);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contact = await _context.Set<Contact>()
                                    .Include(c => c.Image)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        if (contact?.Image != null)
            _context.Remove(contact.Image);

        if (contact != null)
            _context.Remove(contact);

        await _context.SaveChangesAsync();
        TempData["success"] = "مخاطب با موفقیت حذف شد";
        return RedirectToAction(nameof(Index));
    }

    #endregion

    bool IsExistPhoneNumber(string phoneNumber, int? id) => _context.Set<Contact>().Any(c =>
                                                                                        c.PhoneNumber == phoneNumber &&
                                                                                        (id == null || c.Id != id));
}