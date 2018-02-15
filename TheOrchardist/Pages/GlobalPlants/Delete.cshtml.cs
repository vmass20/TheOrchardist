using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.GlobalPlants
{
    public class DeleteModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public DeleteModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GlobalPlantList GlobalPlantList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GlobalPlantList = await _context.GlobalPlantLists.SingleOrDefaultAsync(m => m.ID == id);

            if (GlobalPlantList == null)
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

            GlobalPlantList = await _context.GlobalPlantLists.FindAsync(id);

            if (GlobalPlantList != null)
            {
                _context.GlobalPlantLists.Remove(GlobalPlantList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
