using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TheOrchardist.Controllers;
using TheOrchardist.Data;
using TheOrchardist.Services;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TheOrchardist.Pages.Account
{
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly IEmailSender _emailSender;
    public IList<AuthenticationScheme> ExternalLogins { get; set; }
    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnError { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginModel> logger,
        IEmailSender emailSender)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
    }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }
      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

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

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      if (!string.IsNullOrEmpty(ErrorMessage))
      {
        ModelState.AddModelError(string.Empty, ErrorMessage);
      }

      ReturnUrl = returnUrl;
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

        var encodedResponse = Request.Form["g-Recaptcha-Response"];

        var reCaptchaGoogle = CaptchaResponse.Validate(encodedResponse);

        if (!reCaptchaGoogle)
        {
          // Clear the existing external cookie to ensure a clean login process
          await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

          ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

          ReturnUrl = returnUrl;
          ModelState.AddModelError(string.Empty,
                           "Please confirm you are human by checking the reCaptcha.");
          return Page(); //RedirectToPage("/Register" ,"Captcha" , new { ModelState });
        }

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {        
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
          await  _emailSender.SendEmailConfirmationAsync("Please Confirm your Account", code);
          _logger.LogInformation("User created a new account with password.");
          await _signInManager.SignInAsync(user, isPersistent: false);
          return LocalRedirect(Url.GetLocalUrl(returnUrl));
        }
        else if(!await _userManager.IsEmailConfirmedAsync(user)) 
        {
          ModelState.AddModelError(string.Empty,
                         "You must have a confirmed email to log in.");

          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          //var callbackUrl = Url.Action("ConfirmEmail", "Account",
          //  new { userId = userID, code = code }, protocol: Request.Url.Scheme);
          var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
          await _emailSender.SendEmailConfirmationAsync("Please Confirm your Account", callbackUrl);

          return Page();
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
