using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TheOrchardist.Data;
using TheOrchardist.Pages.GlobalPlants;

namespace TheOrchardist.Controllers
{
  [Route("[controller]/[action]")]
  public class sortOrderController : Controller
  {
    [BindProperty]
    public IList<GlobalPlantList> globalPlantList { get; set; }
    [BindProperty]
    public bool Desc { get; set; }
    private readonly TheOrchardist.Data.OrchardDBContext _context;
    public sortOrderController(OrchardDBContext context) 
    {
      _context = context;
    }
    [HttpGet]
        public IActionResult Index(IList<GlobalPlantList> globalPlantList)
        {

      return View();
        }
    [HttpGet]
    public ActionResult FillCity(IList<GlobalPlantList> GlobalPlantList, bool Desc, int? pageIndex)
    {
      return View();
    }
    [HttpGet]
    public async Task<ActionResult> GetSortedList( IList<GlobalPlantList> GlobalPlantList, bool Desc, int? pageIndex )
    {

      IQueryable<GlobalPlantList> pagedList = from s in _context.GlobalPlantLists
                                              select s;
      PaginatedList<GlobalPlantList> PaginatedList;
      int pageSize = 25;
      PaginatedList = await PaginatedList<GlobalPlantList>.CreateAsync(
      pagedList.AsNoTracking(), pageIndex ?? 1, pageSize);
      // globalPlantList = PaginatedList.ToList();
      // var GlobalPlantList = paginatedList.OrderByDescending(e => e.Name);
      if (Desc == true)
      {
        globalPlantList =  PaginatedList.OrderBy(e => e.Name).ToList();
        Desc = false;
        this.Desc = Desc;
      }
      else
      {
        this.Desc = Desc;
        globalPlantList = PaginatedList.OrderByDescending(e => e.Name).ToList();
        Desc = true;
      }
     // return View(globalPlantList);

     return RedirectToPage("/GlobalPlants/Index","NameOrder", new RouteValueDictionary(new {globalPlantList ,Desc, pageIndex })); //RedirectToPage("/GlobalPlants/Index", globalPlantList);
     // RedirectToPageResult l = RedirectToPage("/GlobalPlants/Index", "GetOrder" );
      //return l;
    }
 [HttpGet]
    public ActionResult GetProductDetails(string productid)
    {
      return null;
    }
  }
}