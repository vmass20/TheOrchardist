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

    GlobalPlantList globalPlantList = new GlobalPlantList();
    UserPlantList userPlantList = new UserPlantList();
    public async Task< IActionResult> OnGet(int? id, string OrchardName)
        {
      if (UserPlantList == null)
      {
        UserPlantList = new UserPlantList();
      }
   
      //if (OrchardNames != null)
      //{ OrchardNamesList = new SelectList(OrchardNames); }
      if (id != null)
      {

        globalPlantList = await _context.GlobalPlantLists.SingleOrDefaultAsync(m => m.ID == id);
        if (OrchardName != null)
        { userPlantList.OrchardName = OrchardName;
          this.OrchardName = OrchardName;
        }
        userPlantList.FruitVariety = globalPlantList.FruitVariety;
        userPlantList.PlantName = globalPlantList.Name;
        userPlantList.Origin = globalPlantList.Origin;
        userPlantList.YearDeveloped = globalPlantList.YearDeveloped;
        userPlantList.Comments = globalPlantList.Comments;
        userPlantList.Use = globalPlantList.Use;
        UserPlantList = userPlantList;
      }
      else
      {
      userPlantList = new UserPlantList();
        if (OrchardName != null)
        { UserPlantList.OrchardName = OrchardName;
          this.OrchardName = OrchardName;
        }


      }
      if (userPlantList == null)
      {
        return NotFound();
      }
      return Page();
        }

        [BindProperty]
        public UserPlantList UserPlantList { get; set; }

    public async Task<IActionResult> OnPostAsync(string OrchardName)
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }
      this.OrchardName = OrchardName;
  
      var userId = _userManager.GetUserId(HttpContext.User);
      UserPlantList.UserID = userId;

      _context.Attach(UserPlantList).State = EntityState.Added;
      _context.UserPlantLists.Add(UserPlantList);

      
      GlobalPlantList plantList = new GlobalPlantList();
      plantList.Name = UserPlantList.PlantName;
      plantList.FruitVariety = UserPlantList.FruitVariety;
      _context.Attach(plantList).State = EntityState.Added;
      _context.GlobalPlantLists.Add(plantList);

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