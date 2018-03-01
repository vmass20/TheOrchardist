using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.UserPlants
{
    public class WhiteboardModel : PageModel
    {
    private readonly Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    [BindProperty]
    public Orchard Orchard { get; set; }
    [BindProperty]
    public string dataURL { get; set; }
    [BindProperty]
    public IList<UserPlantList> UserPlantList { get; set; }
    [BindProperty]
    public string OrchardName { get; set; }
    public WhiteboardModel(Data.OrchardDBContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }
    public async Task<IActionResult> OnGet(int? id, string OrchardName)
    {
      this.OrchardName = OrchardName;
      var userId = _userManager.GetUserId(HttpContext.User);

      if (OrchardName != null)
      {
      UserPlantList = await _context.UserPlantLists.Where(u => u.UserID == userId & u.OrchardName == OrchardName).ToListAsync();

      }
      return Page();
    }


    private bool OrchardExists(int id)
    {
      return _context.Orchards.Any(e => e.OrchardID == id);
    }
    
  }
}