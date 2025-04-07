using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using PBP.DataAccess.Models;

namespace PBP.Controllers;

[Authorize]
public class DynamicListsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;


    public async Task<IActionResult> GetItemsByCategory(string category)
    {
        var items = await _context.Set<DynamicListItem>()
                                    .Where(d => d.Category == category && d.IsActive)
                                    .ToListAsync();

        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(string category, string value)
    {
        var item = new DynamicListItem
        {
            Category = category,
            Value = value,
            IsActive = true
        };

        await _context.AddAsync(item);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DynamicListsController.GetItemsByCategory));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveItem(int id)
    {
        var item = await _context.Set<DynamicListItem>().FindAsync(id);
        if (item != null)
        {
            item.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(DynamicListsController.GetItemsByCategory));
    }
}