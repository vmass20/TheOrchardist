using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheOrchardist.Controllers
{
   
    public class OrchardNameController : Controller
    {
    public string OrchardName { get; set; }
    public string FruitVariety { get; set; }
    // GET: OrchardName
    public ActionResult Index(string OrchardName)
        {
      this.OrchardName = OrchardName;
            return View();
        }
    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetOrchardName(string OrchardName, string FruitVariety)
    {
      this.OrchardName = OrchardName;
      this.FruitVariety = FruitVariety;
      return View();
    }


  }
}