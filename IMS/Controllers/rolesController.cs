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
    public class rolesController : Controller
    {
        private readonly IMSDBContext _context;

        public rolesController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: roles
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "users");
            }
            else
            {
                return View(await _context.RoleTb.ToListAsync());
            }
        }

        // GET: roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleTb = await _context.RoleTb
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleTb == null)
            {
                return NotFound();
            }

            return View(roleTb);
        }

        // GET: roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,RoleStatus,PermissionCreateUpdate,PermissionDelete,PermissionView,CreatedAt")] RoleTb roleTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleTb);
        }

        // GET: roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleTb = await _context.RoleTb.FindAsync(id);
            if (roleTb == null)
            {
                return NotFound();
            }
            return View(roleTb);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,RoleStatus,PermissionCreateUpdate,PermissionDelete,PermissionView,CreatedAt")] RoleTb roleTb)
        {
            if (id != roleTb.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleTbExists(roleTb.RoleId))
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
            return View(roleTb);
        }

        // GET: roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleTb = await _context.RoleTb
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleTb == null)
            {
                return NotFound();
            }

            return View(roleTb);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleTb = await _context.RoleTb.FindAsync(id);
            _context.RoleTb.Remove(roleTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleTbExists(int id)
        {
            return _context.RoleTb.Any(e => e.RoleId == id);
        }
    }
}
