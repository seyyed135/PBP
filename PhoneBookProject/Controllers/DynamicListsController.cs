using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using PBP.DataAccess.Models;
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
        var categories = await _context.Set<DynamicListItem>()
                                        .Where(d => d.IsActive)
                                        .Select(d => d.Category)
                                        .Distinct()
                                        .ToListAsync();

        ViewBag.Categories = categories;

        if (!string.IsNullOrEmpty(category))
        {
            var items = await _context.Set<DynamicListItem>()
                                      .Where(d => d.IsActive &&
                                             (category == null || d.Category == category))
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
    public IActionResult Create() => View();

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