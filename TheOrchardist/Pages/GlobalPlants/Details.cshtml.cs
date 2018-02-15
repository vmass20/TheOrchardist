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
    public class DetailsModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;

        public DetailsModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

        public GlobalPlantList GlobalPlantList { get; set; }
        [BindProperty]
        public string OrchardName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, string OrchardName)
        {
      this.OrchardName = OrchardName;
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
    }
}
