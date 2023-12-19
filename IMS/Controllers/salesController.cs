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

            ViewData["sttype"] = new SelectList(_context.StateTb, "StateName", "StateName");

            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesId,SalesTyep,SalesDate,CustomerName,CustomerAddress,CustomerMobile,CustomerType,ItemId,Qty,UnitPrice,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,Remark,UserId,CreatedAt")] SalesTb salesTb)
        {
            if (ModelState.IsValid)
            {
                var itemdata = _context.ItemTb.FirstOrDefault(i => i.ItemId == salesTb.ItemId);
                var classdata = _context.ClassTb.FirstOrDefault(c => c.ClassId == itemdata.ItemClassId);
                var gstdata = _context.GstTb.FirstOrDefault(g => g.GstId == classdata.GstId);
                var purchasedata = _context.PurchaseTb.FirstOrDefault(p => p.ItemId == salesTb.ItemId);

                //var purdata = _context.PurchaseTb.Count();

                int total_sold_item = _context.SalesTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty); 
                int total_purchase_item = _context.PurchaseTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty);


                if ((salesTb.Qty+total_sold_item) <= total_purchase_item)
                {
                    if (salesTb.UnitPrice >= itemdata.ItemSalesRate)
                    {
                        salesTb.UnitPrice = salesTb.UnitPrice;
                        salesTb.Total1 = salesTb.UnitPrice * salesTb.Qty;
                        salesTb.Total2 = (double)(salesTb.Total1 - (salesTb.Discount * salesTb.Total1) / 100);
                        salesTb.GstId = gstdata.GstId;

                        if (salesTb.CustomerType == "gujarat")
                        {
                            salesTb.CgstAmount = (salesTb.Total2 * (float)gstdata.Cgst) / 100;
                            salesTb.SgstAmount = (salesTb.Total2 * (float)gstdata.Sgst) / 100;
                            salesTb.IgstAmount = 0;
                        }
                        else
                        {
                            salesTb.CgstAmount = 0;
                            salesTb.SgstAmount = 0;
                            salesTb.IgstAmount = (salesTb.Total2 * (float)gstdata.Igst) / 100;
                        }

                        salesTb.Total3 = salesTb.Total2 + salesTb.CgstAmount + salesTb.SgstAmount + salesTb.IgstAmount;
                        salesTb.UserId = (int)HttpContext.Session.GetInt32("UserId");


                        _context.Add(salesTb);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        //salesTb.UnitPrice = itemdata.ItemSalesRate;
                        //price <=sellingprice
                        return RedirectToAction(nameof(Index));
                    }

                }
                else
                {
                    //qty not available
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", salesTb.GstId);
            ViewData["ItemId"] = new SelectList(_context.ItemTb, "ItemId", "ItemName", salesTb.ItemId);
            ViewData["UserId"] = new SelectList(_context.UserTb, "UserId", "Password", salesTb.UserId);
            ViewData["sttype"] = new SelectList(_context.StateTb, "StateName", "StateName", salesTb.CustomerType);
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
        public async Task<IActionResult> Edit(int id, [Bind("SalesId,SalesTyep,SalesDate,CustomerName,CustomerAddress,CustomerMobile,CustomerType,ItemId,Qty,UnitPrice,Total1,Discount,Total2,GstId,CgstAmount,SgstAmount,IgstAmount,Total3,Remark,UserId,CreatedAt")] SalesTb salesTb)
        {
            if (id != salesTb.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var itemdata = _context.ItemTb.FirstOrDefault(i => i.ItemId == salesTb.ItemId);
                    var classdata = _context.ClassTb.FirstOrDefault(c=>c.ClassId==itemdata.ItemClassId);
                    var gstdata = _context.GstTb.FirstOrDefault(g => g.GstId == classdata.GstId);
                    var purchasedata = _context.PurchaseTb.FirstOrDefault(p => p.ItemId == salesTb.ItemId);


                    //int total_sold_item = _context.SalesTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty);
                    //int total_purchase_item = _context.PurchaseTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty);


                    //if ((salesTb.Qty + total_sold_item) <= total_purchase_item)
                    //{
                    //    if (salesTb.UnitPrice >= itemdata.ItemSalesRate)
                    //    {
                    //        salesTb.UnitPrice = salesTb.UnitPrice;
                    //        salesTb.Total1 = salesTb.UnitPrice * salesTb.Qty;
                    //        salesTb.Total2 = (double)(salesTb.Total1 - (salesTb.Discount * salesTb.Total1) / 100);
                    //        salesTb.GstId = gstdata.GstId;

                    //        if (salesTb.CustomerType == "gujarat")
                    //        {
                    //            salesTb.CgstAmount = (salesTb.Total2 * (float)gstdata.Cgst) / 100;
                    //            salesTb.SgstAmount = (salesTb.Total2 * (float)gstdata.Sgst) / 100;
                    //            salesTb.IgstAmount = 0;
                    //        }
                    //        else
                    //        {
                    //            salesTb.CgstAmount = 0;
                    //            salesTb.SgstAmount = 0;
                    //            salesTb.IgstAmount = (salesTb.Total2 * (float)gstdata.Igst) / 100;
                    //        }

                    //        salesTb.Total3 = salesTb.Total2 + salesTb.CgstAmount + salesTb.SgstAmount + salesTb.IgstAmount;
                    //        salesTb.UserId = (int)HttpContext.Session.GetInt32("UserId");


                    //        _context.Add(salesTb);
                    //        await _context.SaveChangesAsync();
                    //        return RedirectToAction(nameof(Index));
                    //    }
                    //    else
                    //    {
                    //        //salesTb.UnitPrice = itemdata.ItemSalesRate;
                    //        //price <=sellingprice
                    //        return RedirectToAction(nameof(Index));
                    //    }

                    //}
                    //else
                    //{
                    //    //qty not available
                    //    return RedirectToAction(nameof(Index));
                    //}
                    ////_context.Update(salesTb);
                    ////await _context.SaveChangesAsync();
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
                //return RedirectToAction(nameof(Index));
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
