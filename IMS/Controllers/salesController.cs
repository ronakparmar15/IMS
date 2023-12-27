using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using Microsoft.AspNetCore.Http;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.IO;

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

        //report
        //r2
        public async Task<IActionResult> Report2()
        {
            var purchaseData = from purchase in _context.PurchaseTb
                               group purchase by purchase.ItemId into grouped
                               select new
                               {
                                   ItemId = grouped.Key,
                                   PurchaseQty = grouped.Sum(p => p.Qty),
                                   PurchaseTotal3 = (decimal)grouped.Sum(p => p.Total3)
                               };

            var saleData = from sale in _context.SalesTb
                           group sale by sale.ItemId into grouped
                           select new
                           {
                               ItemId = grouped.Key,
                               SalesQty = grouped.Sum(s => s.Qty),
                               SalesTotal3 = (decimal)grouped.Sum(s => s.Total3),
                               
                           };

            var combinedData = from p in purchaseData
                               join s in saleData on p.ItemId equals s.ItemId into temp
                               from s in temp.DefaultIfEmpty()
                               select new stokes
                               {
                                   ItemId = p.ItemId,
                                   PurchaseQty = p.PurchaseQty,
                                   PurchaseTotal3 = p.PurchaseTotal3,
                                   SalesQty = s.SalesQty,
                                   NetQty=p.PurchaseQty-s.SalesQty,
                                   NetTotal= (p.PurchaseQty - s.SalesQty) ,
                                   SalesTotal3 = s.SalesTotal3
                               };

            return View( await combinedData.ToListAsync());
        }

        //pdf
        public List<stokes> GetReport()
        {
            List<stokes> data = new List<stokes>();
            var purchaseData = from purchase in _context.PurchaseTb
                               group purchase by purchase.ItemId into grouped
                               select new
                               {
                                   ItemId = grouped.Key,
                                   PurchaseQty = grouped.Sum(p => p.Qty),
                                   PurchaseTotal3 = (decimal)grouped.Sum(p => p.Total3)
                               };

            var saleData = from sale in _context.SalesTb
                           group sale by sale.ItemId into grouped
                           select new
                           {
                               ItemId = grouped.Key,
                               SalesQty = grouped.Sum(s => s.Qty),
                               SalesTotal3 = (decimal)grouped.Sum(s => s.Total3),
                               SalePrice = grouped.Average(s => s.UnitPrice)
                           };

            var combinedData = from p in purchaseData
                               join s in saleData on p.ItemId equals s.ItemId into temp
                               from s in temp.DefaultIfEmpty()
                               select new stokes
                               {
                                   ItemId = p.ItemId,
                                   PurchaseQty = p.PurchaseQty,
                                   PurchaseTotal3 = p.PurchaseTotal3,
                                   SalesQty = s.SalesQty,
                                   NetQty = p.PurchaseQty - s.SalesQty,
                                   NetTotal = (decimal)((p.PurchaseQty - s.SalesQty)*s.SalePrice),
                                   SalesTotal3 = s.SalesTotal3
                               };

            data = combinedData.ToList();
            return data;
        }

        [HttpGet("DownloadPdf")]
        public IActionResult DownloadPdf()
        {
            var document = new PdfDocument();
            decimal total = 0;
            decimal tp = 0;
            decimal ts = 0;
            List<stokes> sdata = GetReport();

            string htmlstring = "<table style='width:500px; border:solid; border-width:1px;'> <thead style='border:solid balck; border-width:2px;'> <tr>";
            htmlstring += "<th style='width:10%; text-align:left;'>ItemId</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Purchase Qty</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Sales Qty</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Qty In-hand</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Total Purchase(Rs)</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Total Sales(Rs)</th>";
            htmlstring += "<th style='width:10%; text-align:left;'>Pending Amount(Rs)</th></tr></thead><tbody>";


            foreach (stokes obj in sdata)
            {
                htmlstring += "<tr><td style='width:10%;text-align:left;'>" + obj.ItemId.ToString() + "</td>";
                htmlstring += "<td style='width:10%; text-align:left; '>" + obj.PurchaseQty.ToString() + "</td>";
                htmlstring += "<td style='width:10%; text-align:left;'>" + obj.SalesQty.ToString() + "</td>";
                htmlstring += "<td style='width:10%;text-align:left;' > " + obj.NetQty.ToString() + " </ td > ";
                htmlstring += "<td style='width:10%;text-align:left;' > " + obj.PurchaseTotal3.ToString() + " </ td > ";
                htmlstring += "<td style='width:10%;text-align:left;' > " + obj.SalesTotal3.ToString() + " </ td > ";
                htmlstring += "<td style='width:10%;text-align:left;' > " + obj.NetTotal.ToString() + " </ td ></ tr >";

                total += obj.NetTotal;
                tp += obj.PurchaseTotal3;
                ts += obj.SalesTotal3;
            }

            htmlstring += "<tr><td>Total:</td><td></td><td></td><td></td><td>" + tp+"</td><td>"+ts+"</td><td style='width:7%;text-align:right;'>" + total + "</td></tr></tbody></table> ";


            PdfGenerator.AddPdfPages(document, htmlstring, PageSize.A4);
            byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                res = ms.ToArray();
            }
            return File(res, "application/pdf");
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

            return View( salesTb);
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
                        TempData["InvalidData"] = "Insufficient Price!";
                        //Console.WriteLine(ex.Message);
                        return RedirectToAction(nameof(Index));
                    }

                }
                else
                {
                    //qty not available
                    TempData["InvalidData"] = "Insufficient Quantity!";
                    //Console.WriteLine(ex.Message);
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
                    var classdata = _context.ClassTb.FirstOrDefault(c => c.ClassId == itemdata.ItemClassId);
                    var gstdata = _context.GstTb.FirstOrDefault(g => g.GstId == classdata.GstId);
                    var purchasedata = _context.PurchaseTb.FirstOrDefault(p => p.ItemId == salesTb.ItemId);


                    int total_sold_item = _context.SalesTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty);
                    int total_purchase_item = _context.PurchaseTb.Where(i => i.ItemId == salesTb.ItemId).Sum(i => i.Qty);


                    if ((salesTb.Qty + total_sold_item) <= total_purchase_item)
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

                    //_context.Update(salesTb);
                    //await _context.SaveChangesAsync();
                    

                    //----
                    _context.Add(salesTb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
