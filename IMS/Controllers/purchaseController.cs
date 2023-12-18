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
    public class purchaseController : Controller
    {
        private readonly IMSDBContext _context;

        public purchaseController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: purchase
        public async Task<IActionResult> Index()
        {
            var iMSDBContext = _context.PurchaseTb.Include(p => p.Gst).Include(p => p.Item).Include(p => p.Sup).Include(p => p.User);
            return View(await iMSDBContext.ToListAsync());
        }

        // GET: purchase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTb = await _context.PurchaseTb
                .Include(p => p.Gst)
                .Include(p => p.Item)
                .Include(p => p.Sup)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PurId == id);
            if (purchaseTb == null)
            {
                return NotFound();
            }

            return View(purchaseTb);
        }

        // GET: purchase/Create
        public IActionResult Create()
        {
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName");
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName");
            ViewData["SupId"] = new SelectList(_context.SupplierTb, "SupId", "SupName");
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password");
            return View();
        }

        // POST: purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurId,PerDate,SupId,ItemId,Qty,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,UserId,Remark,CreatedAt")] PurchaseTb purchaseTb)
        {
            if (ModelState.IsValid)
            {
                var itemdata = _context.ItemTb.FirstOrDefault(p => p.ItemId == purchaseTb.ItemId);
                var classdata=_context.ClassTb.FirstOrDefault(c=>c.ClassId==itemdata.ItemClassId);
                var gstdata = _context.GstTb.FirstOrDefault(g => g.GstId==classdata.GstId);
                var supplierdata=_context.SupplierTb.FirstOrDefault(s=>s.SupId==purchaseTb.SupId);

                var cgst = gstdata.Cgst;
                purchaseTb.Total1 = purchaseTb.Qty *itemdata.ItemPurchaseRate;
                purchaseTb.Total2 = (double)(purchaseTb.Total1 - (purchaseTb.Discount * purchaseTb.Total1) / 100);
                purchaseTb.GstId=gstdata.GstId;
                if(supplierdata.SupType=="gujarat")
                {
                    purchaseTb.CgstAmount = (purchaseTb.Total2 * (float)gstdata.Cgst) / 100;
                    purchaseTb.SgstAmount = (purchaseTb.Total2 * (float)gstdata.Sgst) / 100;
                    purchaseTb.IgstAmount = 0;
                }
                else
                {
                    purchaseTb.CgstAmount = 0;
                    purchaseTb.SgstAmount = 0;
                    purchaseTb.IgstAmount =  (purchaseTb.Total2 * (float)gstdata.Igst) / 100;
                }

                purchaseTb.Total3 =purchaseTb.Total2+ purchaseTb.CgstAmount + purchaseTb.SgstAmount + purchaseTb.IgstAmount;
                purchaseTb.UserId = (int)HttpContext.Session.GetInt32("UserId");

                _context.Add(purchaseTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", purchaseTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", purchaseTb.ItemId);
            ViewData["SupId"] = new SelectList(_context.SupplierTb, "SupId", "SupName", purchaseTb.SupId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", purchaseTb.UserId);
            return View(purchaseTb);
        }

        // GET: purchase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTb = await _context.PurchaseTb.FindAsync(id);
            if (purchaseTb == null)
            {
                return NotFound();
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", purchaseTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", purchaseTb.ItemId);
            ViewData["SupId"] = new SelectList(_context.SupplierTb, "SupId", "SupName", purchaseTb.SupId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", purchaseTb.UserId);
            return View(purchaseTb);
        }

        // POST: purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurId,PerDate,SupId,ItemId,Qty,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,UserId,Remark,CreatedAt")] PurchaseTb purchaseTb)
        {
            if (id != purchaseTb.PurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseTbExists(purchaseTb.PurId))
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
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", purchaseTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", purchaseTb.ItemId);
            ViewData["SupId"] = new SelectList(_context.SupplierTb, "SupId", "SupName", purchaseTb.SupId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", purchaseTb.UserId);
            return View(purchaseTb);
        }

        // GET: purchase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTb = await _context.PurchaseTb
                .Include(p => p.Gst)
                .Include(p => p.Item)
                .Include(p => p.Sup)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PurId == id);
            if (purchaseTb == null)
            {
                return NotFound();
            }

            return View(purchaseTb);
        }

        // POST: purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseTb = await _context.PurchaseTb.FindAsync(id);
            _context.PurchaseTb.Remove(purchaseTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseTbExists(int id)
        {
            return _context.PurchaseTb.Any(e => e.PurId == id);
        }
    }
}
