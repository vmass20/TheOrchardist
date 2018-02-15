using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.Account.UserPlants
{
    public class IndexModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public IndexModel(UserManager<ApplicationUser> userManager,TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
      _userManager = userManager;
    }
        [BindProperty]
        public IList<UserPlantList> UserPlantList { get;set; }
        public Orchard Orchard { get; set; }
        [BindProperty]
        public string OrchardName { get; set; }

    public async Task OnGetAsync(int? id, string OrchardName)
        {
      this.OrchardName = OrchardName;
      var userId = _userManager.GetUserId(HttpContext.User);

      if (OrchardName == null)
      {
        UserPlantList = await _context.UserPlantLists.Where(u => u.UserID == userId).ToListAsync();


      }
      else
      {
        UserPlantList = await _context.UserPlantLists.Where(u => u.UserID == userId & u.OrchardName == OrchardName).ToListAsync();
   
      }

      this.OrchardName = OrchardName;  
        }
    public void OnPostAync(int? id, string OrchardName)
    {
      this.OrchardName = OrchardName;
   ;
    }
  }
}
