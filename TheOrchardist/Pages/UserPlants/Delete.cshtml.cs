using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.Account.UserPlants
{
    public class DeleteModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public DeleteModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserPlantList UserPlantList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserPlantList = await _context.UserPlantLists.SingleOrDefaultAsync(m => m.OrchardID == id);

            if (UserPlantList == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserPlantList = await _context.UserPlantLists.FindAsync(id);

            if (UserPlantList != null)
            {
                _context.UserPlantLists.Remove(UserPlantList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
