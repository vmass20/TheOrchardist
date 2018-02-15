using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.Account.UserPlants
{
    public class EditModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
        [BindProperty]
        public string OrchardName { get; set; }
        
        public EditModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserPlantList UserPlantList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string OrchardName)
        {
            if (id == null)
            {
                return NotFound();
            }
      this.OrchardName = OrchardName;
            UserPlantList = await _context.UserPlantLists.SingleOrDefaultAsync(m => m.OrchardID == id);

            if (UserPlantList == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string OrchardName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserPlantList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPlantListExists(UserPlantList.OrchardID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { OrchardName });
        }

        private bool UserPlantListExists(int id)
        {
            return _context.UserPlantLists.Any(e => e.OrchardID == id);
        }
    }
}
