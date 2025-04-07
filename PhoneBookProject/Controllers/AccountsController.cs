using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PBP.DataAccess.Models;
using PBP.ViewModels;

namespace PBP.Controllers;

public class AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ActivityLogService activityLogService) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly ActivityLogService _activityLogService = activityLogService;

    #region Register User

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = new IdentityUser
        {
            UserName = viewModel.UserName.Trim(),
            Email = viewModel.Email
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
        {
            if (user.Email == "dev.pro@gmail.com")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            await _activityLogService.LogActivityAsync(user.Id, "User Registered");
            return RedirectToAction(nameof(ContactsController.Index), "Contacts");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(viewModel);
    }

    #endregion

    #region Login User

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var result = await _signInManager.PasswordSignInAsync(
            viewModel.UserName, viewModel.Password, viewModel.RememberMe, false);

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
            await _activityLogService.LogActivityAsync(user.Id, "User logged In");

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ContactsController.Index), "Contacts");
        }

        ModelState.AddModelError(string.Empty, "تلاش برای ورود نامعتبر است.");
        return View(viewModel);
    }

    #endregion

    #region Logout User

    public async Task<IActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);

        await _signInManager.SignOutAsync();

        if (user != null)
            await _activityLogService.LogActivityAsync(user.Id, "User logged Out");

        return RedirectToAction("Login");
    }

    #endregion
}