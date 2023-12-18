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
    public class salesController : Controller
    {
        private readonly IMSDBContext _context;

        public salesController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: sales
        public async Task<IActionResult> Index()
        {
            var iMSDBContext = _context.SalesTb.Include(s => s.Gst).Include(s => s.Item).Include(s => s.User);
            return View(await iMSDBContext.ToListAsync());
        }

        // GET: sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTb = await _context.SalesTb
                .Include(s => s.Gst)
                .Include(s => s.Item)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesTb == null)
            {
                return NotFound();
            }

            return View(salesTb);
        }

        // GET: sales/Create
        public IActionResult Create()
        {
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName");
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName");
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesId,SalesTyep,SalesDate,CustomerName,CustomerAddress,CustomerMobile,ItemId,Qty,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,Remark,UserId,CreatedAt")] SalesTb salesTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", salesTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", salesTb.ItemId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", salesTb.UserId);
            return View(salesTb);
        }

        // GET: sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTb = await _context.SalesTb.FindAsync(id);
            if (salesTb == null)
            {
                return NotFound();
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", salesTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", salesTb.ItemId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", salesTb.UserId);
            return View(salesTb);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesId,SalesTyep,SalesDate,CustomerName,CustomerAddress,CustomerMobile,ItemId,Qty,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,Remark,UserId,CreatedAt")] SalesTb salesTb)
        {
            if (id != salesTb.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTbExists(salesTb.SalesId))
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
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", salesTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", salesTb.ItemId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", salesTb.UserId);
            return View(salesTb);
        }

        // GET: sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTb = await _context.SalesTb
                .Include(s => s.Gst)
                .Include(s => s.Item)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesTb == null)
            {
                return NotFound();
            }

            return View(salesTb);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTb = await _context.SalesTb.FindAsync(id);
            _context.SalesTb.Remove(salesTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTbExists(int id)
        {
            return _context.SalesTb.Any(e => e.SalesId == id);
        }
    }
}
