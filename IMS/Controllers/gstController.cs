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
    public class gstController : Controller
    {
        private readonly IMSDBContext _context;

        public gstController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: gst
        public async Task<IActionResult> Index()
        {
            return View(await _context.GstTb.ToListAsync());
        }

        // GET: gst/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gstTb = await _context.GstTb
                .FirstOrDefaultAsync(m => m.GstId == id);
            if (gstTb == null)
            {
                return NotFound();
            }

            return View(gstTb);
        }

        // GET: gst/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: gst/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GstId,GstName,Cgst,Sgst,Igst,Active,CreatedAt")] GstTb gstTb)
        {
            if (ModelState.IsValid)
            {
                gstTb.Igst = gstTb.Cgst + gstTb.Sgst;
                _context.Add(gstTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gstTb);
        }

        // GET: gst/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gstTb = await _context.GstTb.FindAsync(id);
            if (gstTb == null)
            {
                return NotFound();
            }
            return View(gstTb);
        }

        // POST: gst/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GstId,GstName,Cgst,Sgst,Igst,Active,CreatedAt")] GstTb gstTb)
        {
            if (id != gstTb.GstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gstTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GstTbExists(gstTb.GstId))
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
            return View(gstTb);
        }

        // GET: gst/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gstTb = await _context.GstTb
                .FirstOrDefaultAsync(m => m.GstId == id);
            if (gstTb == null)
            {
                return NotFound();
            }

            return View(gstTb);
        }

        // POST: gst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gstTb = await _context.GstTb.FindAsync(id);
            _context.GstTb.Remove(gstTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GstTbExists(int id)
        {
            return _context.GstTb.Any(e => e.GstId == id);
        }
    }
}
