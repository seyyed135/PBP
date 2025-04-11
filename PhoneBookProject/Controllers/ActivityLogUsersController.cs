using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using PBP.DataAccess.Models;

[Authorize]
public class ActivityLogUsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<IdentityUser> _userManager = userManager;


    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var isAdmin = await _userManager.IsInRoleAsync(currentUser!, "Admin");

        var logs = isAdmin
            ? await _context.Set<ActivityLog>().Include(a => a.User).OrderByDescending(a => a.Timestamp).ToListAsync()
            : await _context.Set<ActivityLog>().Where(a => a.UserId == currentUser!.Id).OrderByDescending(l => l.Timestamp).ToListAsync();

        return View(logs);
    }
}