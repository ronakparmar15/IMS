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
    public class suppliersController : Controller
    {
        private readonly IMSDBContext _context;

        public suppliersController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: suppliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SupplierTb.ToListAsync());
        }

        // GET: suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierTb = await _context.SupplierTb
                .FirstOrDefaultAsync(m => m.SupId == id);
            if (supplierTb == null)
            {
                return NotFound();
            }

            return View(supplierTb);
        }

        // GET: suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupId,SupName,SupAddress,SupType,SupMobile,SupGstNumber")] SupplierTb supplierTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierTb);
        }

        // GET: suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierTb = await _context.SupplierTb.FindAsync(id);
            if (supplierTb == null)
            {
                return NotFound();
            }
            return View(supplierTb);
        }

        // POST: suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupId,SupName,SupAddress,SupType,SupMobile,SupGstNumber")] SupplierTb supplierTb)
        {
            if (id != supplierTb.SupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierTbExists(supplierTb.SupId))
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
            return View(supplierTb);
        }

        // GET: suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierTb = await _context.SupplierTb
                .FirstOrDefaultAsync(m => m.SupId == id);
            if (supplierTb == null)
            {
                return NotFound();
            }

            return View(supplierTb);
        }

        // POST: suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplierTb = await _context.SupplierTb.FindAsync(id);
            _context.SupplierTb.Remove(supplierTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierTbExists(int id)
        {
            return _context.SupplierTb.Any(e => e.SupId == id);
        }
    }
}
