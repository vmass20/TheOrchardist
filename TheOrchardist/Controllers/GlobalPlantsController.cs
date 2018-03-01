using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace TheOrchardist.Controllers
{
    public class GlobalPlantsController : Controller
    {
    private readonly TheOrchardist.Data.OrchardDBContext _context;
    public GlobalPlantsController(TheOrchardist.Data.OrchardDBContext orchardDBContext)
    {
      _context = orchardDBContext;
    }
    public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
    {
     
      return View();
    }

    [HttpPost]
    public ActionResult SaveMap(string dataURL)
    {
      
      return View();
    }
    //[HttpPost]
    //public ActionResult Save(Microsoft.AspNetCore.Http.Extensions.HttpRequestMultipartExtensions Attachment)
    //{
    //  //do stuff
    //  return View();
    //}
  }
}