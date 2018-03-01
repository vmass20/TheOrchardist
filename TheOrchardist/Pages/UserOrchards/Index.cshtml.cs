using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;
using System.Drawing;

namespace TheOrchardist.Pages.UserOrchards
{
  
  public class IndexModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public IList<UserPlantList> UserPlantList { get; set; }

    public IndexModel(TheOrchardist.Data.OrchardDBContext context,   UserManager<ApplicationUser> userManager)
        {
            _context = context;
           _userManager = userManager;
        }

        [BindProperty]
        public string OrchardName { get; set; }

        [BindProperty]
        public string UserID { get; set; }

        public IList<Orchard> Orchard { get;set; }

        public async Task OnGetAsync(string UserID, String OrchardName)
        {
      this.OrchardName = OrchardName;
            Orchard = await _context.Orchards.Where(x => x.UserID == _userManager.GetUserId(User)).ToListAsync();
        }

        public void OnPostAync(int? id, string OrchardName)
        {
          this.OrchardName = OrchardName;
        }
    }
}
