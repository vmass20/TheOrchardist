using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheOrchardist.Controllers
{
    public class PageListCountController : Controller
    {
    private readonly TheOrchardist.Data.OrchardDBContext _context;

    // GET: PageListCount

    public PageListCountController(Data.OrchardDBContext context)
    {
      _context = context;

    }
    public ActionResult Index()
        {
            return View();
        }
   
    public ActionResult ResultViewListCount(string lala)
    {
      string[] results = lala.Split(';');
      string orchardname = results[0];
      int id = Convert.ToInt32(results[1]);

      return RedirectToPage("/GlobalPlants/Index", "ResultViewListCount", new { id , orchardname});
    }

        // GET: PageListCount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PageListCount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PageListCount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PageListCount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PageListCount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PageListCount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PageListCount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}