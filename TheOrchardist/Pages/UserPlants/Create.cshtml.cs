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
    public class CreateModel : PageModel
    {
        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(UserManager<ApplicationUser> userManager,TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
      _userManager = userManager;
        }
        [BindProperty]
        public string OrchardName { get; set; }


   [BindProperty]
    public UserPlantList UserPlantList { get; set; }

    public IActionResult OnGetAsync(int? id, string OrchardName)
        {
      //UserPlantList = new UserPlantList();
      this.OrchardName = OrchardName;
      var userId = _userManager.GetUserId(HttpContext.User);


      this.UserPlantList = new UserPlantList();

      return Page();
    }

    public async Task<IActionResult> OnPost(int? id, string OrchardName, UserPlantList userPlantList)
    {

      this.OrchardName = OrchardName;
      var userId = _userManager.GetUserId(HttpContext.User);
      UserPlantList.UserID = userId;

      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(UserPlantList).State = EntityState.Added;
      _context.UserPlantLists.Add(UserPlantList);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserPlantListExists(UserPlantList.ID))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      // await SaveOrchard();
      return RedirectToPage("./Index", new { this.OrchardName });
    }

    private bool UserPlantListExists(int id)
    {
      return _context.UserPlantLists.Any(e => e.ID == id);
    }
  }
}