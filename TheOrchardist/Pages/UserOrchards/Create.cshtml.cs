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
            if (!ModelState.IsValid)
            {
                return Page();
            }
      ApplicationUser currentUser = await _userManager.GetUserAsync(this.User);
      this.Orchard.UserID = await _userManager.GetUserIdAsync(currentUser);
            _context.Orchards.Add(Orchard);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}