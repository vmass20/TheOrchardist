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
    public class DetailsModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public DetailsModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

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
    }
}
