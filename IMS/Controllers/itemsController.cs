using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using Microsoft.AspNetCore.Http;

namespace IMS.Controllers
{
    public class itemsController : Controller
    {
        private readonly IMSDBContext _context;

        public itemsController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: items
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "users");
            }
            else
            {
                var iMSDBContext = _context.ItemTb.Include(i => i.ItemClass);
                return View(await iMSDBContext.ToListAsync());
            }
            
        }

        // GET: items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTb = await _context.ItemTb
                .Include(i => i.ItemClass)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (itemTb == null)
            {
                return NotFound();
            }

            return View(itemTb);
        }

        // GET: items/Create
        public IActionResult Create()
        {
            ViewData["ItemClassId"] = new SelectList(_context.ClassTb, "ClassId", "ClassName");
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemPurchaseRate,ItemSalesRate,ItemStatus,ItemClassId,CreatedAt")] ItemTb itemTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemClassId"] = new SelectList(_context.ClassTb, "ClassId", "ClassName", itemTb.ItemClassId);
            return View(itemTb);
        }

        // GET: items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTb = await _context.ItemTb.FindAsync(id);
            if (itemTb == null)
            {
                return NotFound();
            }
            ViewData["ItemClassId"] = new SelectList(_context.ClassTb, "ClassId", "ClassName", itemTb.ItemClassId);
            return View(itemTb);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemPurchaseRate,ItemSalesRate,ItemStatus,ItemClassId,CreatedAt")] ItemTb itemTb)
        {
            if (id != itemTb.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTbExists(itemTb.ItemId))
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
            ViewData["ItemClassId"] = new SelectList(_context.ClassTb, "ClassId", "ClassName", itemTb.ItemClassId);
            return View(itemTb);
        }

        // GET: items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTb = await _context.ItemTb
                .Include(i => i.ItemClass)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (itemTb == null)
            {
                return NotFound();
            }

            return View(itemTb);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemTb = await _context.ItemTb.FindAsync(id);
            _context.ItemTb.Remove(itemTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTbExists(int id)
        {
            return _context.ItemTb.Any(e => e.ItemId == id);
        }
    }
}
