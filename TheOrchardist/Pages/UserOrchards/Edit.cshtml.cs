using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.UserOrchards
{
    public class EditModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public EditModel(TheOrchardist.Data.OrchardDBContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Orchard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrchardExists(Orchard.OrchardID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrchardExists(int id)
        {
            return _context.Orchards.Any(e => e.OrchardID == id);
        }
    }
}
