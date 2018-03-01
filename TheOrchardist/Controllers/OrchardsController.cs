using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TheOrchardist.Data;

namespace TheOrchardist.Controllers
{


    public class OrchardsController : Controller
    {
        private readonly OrchardDBContext _context;
    [BindProperty]
    public String OrchardName { get; set; }

    public OrchardsController(OrchardDBContext context)
        {
            _context = context;
        }
    // GET: Orchards
    public async Task<IActionResult> Index()
    {

      return View(await _context.Orchards.ToListAsync());
    }

    [HttpPost]
    public async Task<JObject> AddTag([FromBody] JObject data)
    {
      dynamic answer = new JObject();
      var orchardvalues = data.Last.Values();
      var orchardN = orchardvalues.ElementAt(0);
      var tagvalues = data.First.Values();
      var tag = tagvalues.ElementAt(0);
      var orchard = await _context.Orchards.SingleOrDefaultAsync(m => m.OrchardName == orchardN.ToString());
      orchard.OrchardMapDataUrl = tag.ToString();
      _context.Orchards.Update(orchard);
      await _context.SaveChangesAsync();
      // List<LogRecord> logs = new List<LogRecord>();
      answer.added = true; //fStorage.Tags.AddTag(parentId, tagName, logs);
      return answer;
    }

  [HttpGet]
    public async Task<ActionResult> GetOrchardMap(String OrchardName)
    {
      //var OrchardN = this.OrchardName;
      Orchard orchard = await _context.Orchards.SingleOrDefaultAsync(m => m.OrchardName == OrchardName);
      if (orchard == null)
      {
        return NotFound();
      }

      return Json(orchard.OrchardMapDataUrl);
      
    }
    // GET: Orchards/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orchard = await _context.Orchards
                .SingleOrDefaultAsync(m => m.OrchardID == id);
            if (orchard == null)
            {
                return NotFound();
            }

            return View(orchard);
        }

        // GET: Orchards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orchards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrchardID,UserID,OrchardName,OrchardMapDataUrl")] Orchard orchard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orchard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orchard);
        }

        // GET: Orchards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orchard = await _context.Orchards.SingleOrDefaultAsync(m => m.OrchardID == id);
            if (orchard == null)
            {
                return NotFound();
            }
            return View(orchard);
        }

        // POST: Orchards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrchardID,UserID,OrchardName,OrchardMapDataUrl")] Orchard orchard)
        {
            if (id != orchard.OrchardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orchard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrchardExists(orchard.OrchardID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orchard);
        }

        // GET: Orchards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orchard = await _context.Orchards
                .SingleOrDefaultAsync(m => m.OrchardID == id);
            if (orchard == null)
            {
                return NotFound();
            }

            return View(orchard);
        }

        // POST: Orchards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orchard = await _context.Orchards.SingleOrDefaultAsync(m => m.OrchardID == id);
            _context.Orchards.Remove(orchard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrchardExists(int id)
        {
            return _context.Orchards.Any(e => e.OrchardID == id);
        }
    }
}
