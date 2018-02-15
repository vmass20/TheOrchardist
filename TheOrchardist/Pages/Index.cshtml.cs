using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;

namespace TheOrchardist.Pages
{
    public class IndexModel : PageModel
    {

    [BindProperty]
    public IList<GlobalPlantList> GlobalPlantList { get; set; }
    [BindProperty]
    public IList<UserPlantList> UserPlantList { get; set; }
    [BindProperty]
    public IList<Orchard> Orchard { get; set; }
    [BindProperty]
public string OrchardName { get; set; }
    [BindProperty]
    public string UserID { get; set; }
    private readonly Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public IndexModel(Data.OrchardDBContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task OnGetAsync(string OrchardName)
    {
      GlobalPlantList = await _context.GlobalPlantLists.ToListAsync();
      UserPlantList = await _context.UserPlantLists.ToListAsync();
      Orchard = await _context.Orchards.ToListAsync();
      this.OrchardName = OrchardName;
      this.UserID = _userManager.GetUserId(User);
    }
  }
}
