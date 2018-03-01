using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheOrchardist.Data;

namespace TheOrchardist.Controllers
{
   
    public class OrchardNameController : Controller
    {

    // GET: OrchardName
    public ActionResult Index(int? id, UserPlantList userPlantList, string OrchardName)
        {


            return View();
        }


  }
}