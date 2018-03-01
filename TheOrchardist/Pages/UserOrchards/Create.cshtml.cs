using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.UserOrchards
{
    public class CreateModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public List<Orchard> listOrchard { get; set; }

    public CreateModel(UserManager<ApplicationUser> userManager, TheOrchardist.Data.OrchardDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet(int OrchardID, string UserID)
        {
      return Page();
        }

        [BindProperty]
        public Orchard Orchard { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
      var userId = _userManager.GetUserId(HttpContext.User);
      Orchard.UserID = userId;

      if (!ModelState.IsValid)
            {
                return Page();
            }

  
      _context.Orchards.Add(Orchard);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}