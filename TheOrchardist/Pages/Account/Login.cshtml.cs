using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TheOrchardist.Data;
using TheOrchardist.Services;

namespace TheOrchardist.Pages.Account
{
  public class LoginModel : PageModel
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly IEmailSender _emailSender;
    public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, IEmailSender emailSender)
    {
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
      if (!string.IsNullOrEmpty(ErrorMessage))
      {
        ModelState.AddModelError(string.Empty, ErrorMessage);
      }

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

      ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      ReturnUrl = returnUrl;

      if (ModelState.IsValid)
      {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        var user = await _signInManager.UserManager.FindByNameAsync(Input.Email);
        if (user != null)
        {
          if (!await _signInManager.UserManager.IsEmailConfirmedAsync(user))
          {
            //  _logger = "You must have a confirmed email to log on.";
            var code = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
              
          // await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);
            await _emailSender.SendEmail(callbackUrl, Input);

            // Uncomment to debug locally  
            // ViewBag.Link = callbackUrl;
            ModelState.AddModelError(string.Empty, "You must have a confirmed email to log on."
            + "The confirmation email has been resent to your email account.");

            return Page();
          }
          // This doesn't count login failures towards account lockout
          // To enable password failures to trigger account lockout, set lockoutOnFailure: true
          var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
          if (result.Succeeded)
          {
            _logger.LogInformation("User logged in.");
            return LocalRedirect(Url.GetLocalUrl(returnUrl));
          }
          if (result.RequiresTwoFactor)
          {
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
          }
          if (result.IsLockedOut)
          {
            _logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
          }
          else
          {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
          }
        }
      }
      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
