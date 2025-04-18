using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using PBP.DataAccess.Models;
using PBP.Extensions;
using PBP.ViewModels;

namespace PBP.Controllers;

[Authorize]
public class DynamicListsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ActivityLogService activityLogService) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly ActivityLogService _activityLogService = activityLogService;


    [HttpGet]
    public async Task<IActionResult> Index(string? category)
    {
        var categoryNames = Enum.GetValues(typeof(CategoryName))
                     .Cast<CategoryName>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.GetDisplayName()
                     })
                     .ToList();

        ViewBag.CategoryNames = categoryNames;

        if (!string.IsNullOrEmpty(category))
        {
            var items = await _context.Set<DynamicListItem>()
                                      .Where(d => d.IsActive &&
                                            (string.IsNullOrEmpty(category) || d.Category.ToString() == category))
                                      .ToListAsync();

            ViewBag.SelectedCategory = category;

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
                await _activityLogService.LogActivityAsync(currentUser.Id, "مشاهده لیست پویا");

            return View(items);
        }
        return View(new List<DynamicListItem>());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        var categoryNames = Enum.GetValues(typeof(CategoryName))
                          .Cast<CategoryName>()
                          .Select(c => new SelectListItem
                          {
                              Value = c.ToString(),
                              Text = c.GetDisplayName()
                          })
                          .ToList();

        ViewBag.CategoryNames = categoryNames;

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(DynamicListItemViewModel viewModel)
    {     
        var dynamicListItem = new DynamicListItem();

        if (ModelState.IsValid)
        {
            viewModel.UpdateModel(dynamicListItem);

            await _context.AddAsync(dynamicListItem);

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
                await _activityLogService.LogActivityAsync(currentUser.Id, "افزودن آیتم به لیست پویا");

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Set<DynamicListItem>().FindAsync(id);
        if (item != null)
        {
            item.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(DynamicListsController.Index));
    }
}