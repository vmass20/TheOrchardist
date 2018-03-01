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

namespace TheOrchardist.Pages.GlobalPlants
{
    public class CreateModel : PageModel
    {
    [BindProperty]
    public string OrchardName{ get; set; }

        private readonly TheOrchardist.Data.OrchardDBContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public CreateModel(UserManager<ApplicationUser> userManager,TheOrchardist.Data.OrchardDBContext context)
        {
            _context = context;
      _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(int? id,string OrchardName)
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

        [BindProperty]
        public GlobalPlantList GlobalPlantList { get; set; }

        public async Task<IActionResult> OnPostAsync(string OrchardName)
        {
      UserPlantList userPlantList = new UserPlantList();
      var userId = _userManager.GetUserId(HttpContext.User);
      userPlantList.UserID = userId;
      userPlantList.OrchardName = OrchardName;
      userPlantList.FruitVariety = GlobalPlantList.FruitVariety;
      userPlantList.PlantName = GlobalPlantList.Name;
      userPlantList.Origin = GlobalPlantList.Origin;
      userPlantList.YearDeveloped = GlobalPlantList.YearDeveloped;
      userPlantList.Comments = GlobalPlantList.Comments;
      userPlantList.Use = GlobalPlantList.Use;

      if (!ModelState.IsValid)
            {
                return Page();
            }

      this.OrchardName = OrchardName;

      _context.UserPlantLists.Add(userPlantList);
      await _context.SaveChangesAsync();


           // _context.GlobalPlantLists.Add(GlobalPlantList);
           // await _context.SaveChangesAsync();

            return RedirectToPage("/UserPlants/Index");
        }
    }
}