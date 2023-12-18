using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;

namespace IMS.Controllers
{
    public class statesController : Controller
    {
        private readonly IMSDBContext _context;

        public statesController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: states
        public async Task<IActionResult> Index()
        {
            return View(await _context.StateTb.ToListAsync());
        }

        // GET: states/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateTb = await _context.StateTb
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (stateTb == null)
            {
                return NotFound();
            }

            return View(stateTb);
        }

        // GET: states/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: states/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateName")] StateTb stateTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stateTb);
        }

        // GET: states/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateTb = await _context.StateTb.FindAsync(id);
            if (stateTb == null)
            {
                return NotFound();
            }
            return View(stateTb);
        }

        // POST: states/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateName")] StateTb stateTb)
        {
            if (id != stateTb.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateTbExists(stateTb.StateId))
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
            return View(stateTb);
        }

        // GET: states/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateTb = await _context.StateTb
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (stateTb == null)
            {
                return NotFound();
            }

            return View(stateTb);
        }

        // POST: states/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stateTb = await _context.StateTb.FindAsync(id);
            _context.StateTb.Remove(stateTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateTbExists(int id)
        {
            return _context.StateTb.Any(e => e.StateId == id);
        }
    }
}
