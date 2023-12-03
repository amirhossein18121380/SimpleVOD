using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VOD.Models;
using IEmailSender = VOD.Service.IEmailSender;
namespace VOD.Controllers;

public class IdentityController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IEmailSender _emailSender;
    public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }
    public async Task<IActionResult> SignUp()
    {
        var model = new SignUpViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Identity", new { token, email = user.Email }, Request.Scheme);

                    await _emailSender.SendEmailAsync("amirhossein.gholamitousi@gmail.com", user.Email, "Confirm your email address", $"<p>Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.</p>");

                    return RedirectToAction(nameof(RegistrationConfirmation));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Email address is already taken.");
                return View(model);
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return View("Error");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
    }


    [HttpGet]
    public IActionResult RegistrationConfirmation()
    {
        return View();
    }

    public IActionResult SignIn()
    {
        return View(new SignInViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("VideoList", "Media");
            }
            else
            {
                ModelState.AddModelError("Login", "Cannot login.");
            }
        }
        return View(model);
    }
    public async Task<IActionResult> AccessDenied()
    {
        return RedirectToAction("SignIn");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("SignIn", "Identity");
    }
}
