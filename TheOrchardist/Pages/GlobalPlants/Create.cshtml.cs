using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.GlobalPlants
{
    public class CreateModel : PageModel
    {
    [BindProperty]
    public string OrchardName{ get; set; }

        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public CreateModel(UserManager<ApplicationUser> userManager,TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
      _userManager = userManager;
        }

        public IActionResult OnGet(string OrchardName)
        {
            this.OrchardName = OrchardName;
             return Page();
        }

        [BindProperty]
        public GlobalPlantList GlobalPlantList { get; set; }

        public async Task<IActionResult> OnPostAsync(string OrchardName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
      this.OrchardName = OrchardName;

            _context.GlobalPlantLists.Add(GlobalPlantList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}