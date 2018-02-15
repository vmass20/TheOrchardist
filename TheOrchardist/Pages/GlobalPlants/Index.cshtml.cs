using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;
using TheOrchardist.Controllers;
using Microsoft.AspNetCore.Routing;
namespace TheOrchardist.Pages.GlobalPlants
{
  public class IndexModel : PageModel
  {

    private readonly TheOrchardist.Data.OrchardDBContext _context;
    public DbSet<GlobalPlantList> dbSet;
    public OrchardNameController OrchardNameControllers { get; set; }

    public IndexModel(TheOrchardist.Data.OrchardDBContext context)
    {
      
      _context = context;

    }
    [BindProperty]
    public IList<GlobalPlantList> GlobalPlantList { get; set; }
    [BindProperty]
    public ICollection<GlobalPlantList> globalPlantList { get; set; }

    public PaginatedList<GlobalPlantList> PaginatedList { get; set; }

    public string CurrentFilter { get; set; }
    public string sortOrder { get; set; }
    [BindProperty]
    public bool Desc { get; set; }

    public SelectList FruitTypes { get; set; }
    [BindProperty]
    public string FruitVariety { get; set; }

    public SelectList UseTypes { get; set; }
    [BindProperty]
    public String Use { get; set; }
    [BindProperty]
    public string OrchardName { get; set; }
    [BindProperty]
    public int? pageIndex { get; set; }
    [BindProperty]
    public int? pageSize { get; set; }
    public async Task OnGetAsync(int? id,  string OrchardName, IList<GlobalPlantList> globalPlantList, string sortOrder, string searchString, string currentFilter, string FruitVariety, string Use, int? pageIndex, int? pageSize)
    {
      this.OrchardName = OrchardName;
      this.FruitVariety = FruitVariety;
      this.Use = Use;
      if (pageSize != null)
      {
 this.pageSize = pageSize;
      }
      else
      { this.pageSize = 25; }
     
      this.sortOrder = sortOrder;
      this.pageIndex = pageIndex;
      IQueryable<GlobalPlantList> pagedList = from s in _context.GlobalPlantLists
                                              select s;
                                          
      IQueryable<string> plantQuery = from m in _context.GlobalPlantLists
                                      orderby m.FruitVariety
                                      select m.FruitVariety;
      IQueryable<string> useQuery = from m in _context.GlobalPlantLists where m.Use != null
                                      orderby m.Use
                                      select m.Use;

       
      FruitTypes = new SelectList(await plantQuery.Distinct().ToListAsync());
      UseTypes = new SelectList(await useQuery.Distinct().ToListAsync());
     ////if (itemsPerPage != null)
     //// {
     ////   int tint = Convert.ToInt32(itemsPerPage.SelectedValue.ToString());
     ////   pageSize = (int?)tint;

     ////   // this.itemsPage = (int)pageSize;
     //// }
      //itemsPerPage = GetSelectList((int)this.pageSize);

      if (searchString != null)
      {
        pageIndex = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      CurrentFilter = searchString;

      if (!String.IsNullOrEmpty(FruitVariety) & !String.IsNullOrEmpty(searchString))
      {
        pagedList = pagedList.Where(x => x.FruitVariety == FruitVariety && x.Name.Contains(searchString));
      }
      else if (!String.IsNullOrEmpty(Use))
      {
        pagedList = pagedList.Where(x => x.Use == Use);
      }
      else if (!String.IsNullOrEmpty(searchString))
      {

        pagedList = pagedList.Where(s => s.Name.Contains(searchString));
      }
      else if (!String.IsNullOrEmpty(FruitVariety))
      {

        pagedList = pagedList.Where(s => s.FruitVariety.Contains(FruitVariety));
      }
      else if (!string.IsNullOrEmpty(this.sortOrder))
      {
        pagedList = pagedList.OrderByDescending(e => e.Name);
      }
   
      PaginatedList = await PaginatedList<GlobalPlantList>.CreateAsync(
      pagedList.AsNoTracking(), pageIndex ?? 1, (int)this.pageSize);
      GlobalPlantList = PaginatedList.ToList();
    }
    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult OnGetOrchardName(string OrchardName, string FruitVariety)
    {
      this.OrchardName = OrchardName;
      this.FruitVariety = FruitVariety;
      return Page();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostUseAsync(int? id, string Use, string FruitVariety, string searchString, string OrchardName, int? pageIndex, int? pageSize)
    {
      this.OrchardName = OrchardName;
      IQueryable<string> plantQuery = from m in _context.GlobalPlantLists
                                      orderby m.FruitVariety
                                      select m.FruitVariety;
      IQueryable<string> useQuery = from m in _context.GlobalPlantLists
                                    where m.Use != null
                                    orderby m.Use
                                    select m.Use;

      FruitTypes = new SelectList(await plantQuery.Distinct().ToListAsync(), FruitVariety);
      UseTypes = new SelectList(await useQuery.Distinct().ToListAsync(), Use);
      IQueryable<GlobalPlantList> pagedList = from s in _context.GlobalPlantLists
                                              select s;
      if (!String.IsNullOrEmpty(FruitVariety) & !String.IsNullOrEmpty(searchString) & !String.IsNullOrEmpty(Use))
      {
        pagedList = pagedList.Where(x => x.FruitVariety == FruitVariety && x.Name.Contains(searchString) && x.Use == Use);
      }
      else if (!String.IsNullOrEmpty(FruitVariety) & !String.IsNullOrEmpty(searchString) & String.IsNullOrEmpty(Use))
      {
        pagedList = pagedList.Where(x => x.FruitVariety == FruitVariety && x.Name.Contains(searchString));
      }
      else if (!String.IsNullOrEmpty(FruitVariety) & !String.IsNullOrEmpty(Use) & String.IsNullOrEmpty(searchString))
      {
        pagedList = pagedList.Where(x => x.FruitVariety == FruitVariety && x.Use == Use);
      }
      else if (!String.IsNullOrEmpty(Use) & !String.IsNullOrEmpty(searchString) & String.IsNullOrEmpty(FruitVariety))
      {
        pagedList = pagedList.Where(x => x.Use == Use & x.Name.Contains(searchString));
      }
      else if (!String.IsNullOrEmpty(searchString) & String.IsNullOrEmpty(FruitVariety) & String.IsNullOrEmpty(Use))
      { 
        pagedList = pagedList.Where(s => s.Name.Contains(searchString));
      }
      else if (!String.IsNullOrEmpty(FruitVariety) & String.IsNullOrEmpty(searchString) & String.IsNullOrEmpty(Use))
      {
        pagedList = pagedList.Where(s => s.FruitVariety == FruitVariety);
      }
      else if (!String.IsNullOrEmpty(Use) & String.IsNullOrEmpty(searchString) & String.IsNullOrEmpty(FruitVariety))
      {
        pagedList = pagedList.Where(s => s.Use == Use);
      }

      PaginatedList = await PaginatedList<GlobalPlantList>.CreateAsync(
      pagedList.AsNoTracking(), pageIndex ?? 1, (int)pageSize);
      GlobalPlantList = PaginatedList.ToList();
      return Page();
    }
    public IActionResult OnPostAsync(int? id, string Use, string FruitVariety, string OrchardName)
    {
      
      return RedirectToPage("/UserPlants/Create", "LoadOrchardName",  this.OrchardName = OrchardName);
    }
    [HttpGet]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnGetResultViewListCount(int id, string OrchardName)
    {
      this.OrchardName = OrchardName;
      IQueryable<string> plantQuery = from m in _context.GlobalPlantLists
                                      orderby m.FruitVariety
                                      select m.FruitVariety;
      IQueryable<string> useQuery = from m in _context.GlobalPlantLists
                                    where m.Use != null
                                    orderby m.Use
                                    select m.Use;

      FruitTypes = new SelectList(await plantQuery.Distinct().ToListAsync());
      UseTypes = new SelectList(await useQuery.Distinct().ToListAsync());

      int? pageSize = id;
      if (pageSize != null)
      {
        this.pageSize = pageSize;
      }
     
      IQueryable<GlobalPlantList> pagedList = from s in _context.GlobalPlantLists
                                              select s;
 

     PaginatedList = await PaginatedList<GlobalPlantList>.CreateAsync(
        pagedList.AsNoTracking(), pageIndex ?? 1, (int)pageSize);
      GlobalPlantList = PaginatedList.ToList();
      return Page();
    }

  }
}
