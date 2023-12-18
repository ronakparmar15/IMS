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
    public class classController : Controller
    {
        private readonly IMSDBContext _context;

        public classController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: class
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "users");
            }
            else
            {
                var iMSDBContext = _context.ClassTb.Include(c => c.Gst);
                return View(await iMSDBContext.ToListAsync());
            }
        }

        // GET: class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTb = await _context.ClassTb
                .Include(c => c.Gst)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (classTb == null)
            {
                return NotFound();
            }

            return View(classTb);
        }

        // GET: class/Create
        public IActionResult Create()
        {
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName");
            return View();
        }

        // POST: class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,ClassName,Cgst,Sgst,Igst,ClassStatus,CreatedAt,GstId")] ClassTb classTb)
        {
            if (ModelState.IsValid)
            {
                //888
                var data=_context.GstTb.FirstOrDefault(m => m.GstId == classTb.GstId);

                classTb.Cgst = data.Cgst;
                classTb.Sgst = data.Sgst;
                classTb.Igst= data.Igst;
                
                _context.Add(classTb);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", classTb.GstId);
            return View(classTb);
        }

        // GET: class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTb = await _context.ClassTb.FindAsync(id);
            if (classTb == null)
            {
                return NotFound();
            }
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", classTb.GstId);
            return View(classTb);
        }

        // POST: class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,ClassName,Cgst,Sgst,Igst,ClassStatus,CreatedAt,GstId")] ClassTb classTb)
        {
            if (id != classTb.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //8888
                    var data = _context.GstTb.FirstOrDefault(m => m.GstId == classTb.GstId);

                    classTb.Cgst = data.Cgst;
                    classTb.Sgst = data.Sgst;
                    classTb.Igst = data.Igst;

                    _context.Update(classTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassTbExists(classTb.ClassId))
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
            ViewData["GstId"] = new SelectList(_context.GstTb, "GstId", "GstName", classTb.GstId);
            return View(classTb);
        }

        // GET: class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTb = await _context.ClassTb
                .Include(c => c.Gst)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (classTb == null)
            {
                return NotFound();
            }

            return View(classTb);
        }

        // POST: class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classTb = await _context.ClassTb.FindAsync(id);
            _context.ClassTb.Remove(classTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassTbExists(int id)
        {
            return _context.ClassTb.Any(e => e.ClassId == id);
        }
    }
}
