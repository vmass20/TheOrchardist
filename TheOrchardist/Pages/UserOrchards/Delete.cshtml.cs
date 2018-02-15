using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.UserOrchards
{
    public class DeleteModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public DeleteModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Orchard Orchard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orchard = await _context.Orchards.SingleOrDefaultAsync(m => m.OrchardID == id);

            if (Orchard == null)
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

            Orchard = await _context.Orchards.FindAsync(id);

            if (Orchard != null)
            {
                _context.Orchards.Remove(Orchard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
