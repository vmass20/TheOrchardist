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
    public class DetailsModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
        [BindProperty]
        public string OrchardName{ get; set; }
        public DetailsModel(TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
        }

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
    }
}
