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
                return View(await _context.ClassTb.ToListAsync());
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
            return View();
        }

        // POST: class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,ClassName,Cgst,Sgst,Igst,ClassStatus,CreatedAt")] ClassTb classTb)
        {
            if (ModelState.IsValid)
            {
                classTb.Igst = classTb.Cgst + classTb.Sgst;
                _context.Add(classTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(classTb);
        }

        // POST: class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,ClassName,Cgst,Sgst,Igst,ClassStatus,CreatedAt")] ClassTb classTb)
        {
            if (id != classTb.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    classTb.Igst = classTb.Cgst + classTb.Sgst;
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
