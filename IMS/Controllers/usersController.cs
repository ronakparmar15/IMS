using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace IMS.Controllers
{
    public class usersController : Controller
    {
        private readonly IMSDBContext _context;

        public usersController(IMSDBContext context)
        {
            _context = context;
        }

        // GET: users
        public async Task<IActionResult> Index()
        {
            if(!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var iMSDBContext = _context.UserTb.Include(u => u.Role);
                return View(await iMSDBContext.ToListAsync());
            }
            
        }

        // GET: users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTb = await _context.UserTb
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTb == null)
            {
                return NotFound();
            }

            return View(userTb);
        }

        // GET: users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.RoleTb, "RoleId", "RoleName");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Phone,Status,RoleId,CreatedAt")] UserTb userTb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var udata = _context.UserTb.FirstOrDefault(s => s.Username == userTb.Username);
                    if(udata == null)
                    {
                        _context.Add(userTb);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }else
                    {
                        TempData["userexist"] = "User is already exist!";
                        return RedirectToAction(nameof(Index));
                    }
                }catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                    TempData["userexist"] = "User is already exist!";
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["RoleId"] = new SelectList(_context.RoleTb, "RoleId", "RoleName", userTb.RoleId);
            return View(userTb);
        }

        //GET: users/Login
        public  IActionResult Login()
        {
            ViewData["RoleId"] = new SelectList(_context.RoleTb, "RoleId", "RoleName");
            return View();
        }

        //POST: users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(UserTb newuser)
        {
            try
            {
                var user = _context.UserTb.FirstOrDefault(u => u.Username == newuser.Username && u.Password == newuser.Password && u.RoleId == newuser.RoleId);
                var role = _context.RoleTb.FirstOrDefault(r => r.RoleId == user.RoleId);
                if (user != null)
                {
                    if(user.Status==0)
                    {
                        TempData["InvalidLogin"] = "Ristrict to Login!";
                        return RedirectToAction(nameof(Login));
                    }
                    // Store user's Id in session
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserName", user.Username);
                    HttpContext.Session.SetString("RoleName", role.RoleName.ToUpper());
                    //permission
                    HttpContext.Session.SetInt32("createandupdate", role.PermissionCreateUpdate);
                    HttpContext.Session.SetInt32("delete", role.PermissionDelete);
                    HttpContext.Session.SetInt32("view", role.PermissionView);

                    // return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["InvalidLogin"] = "Invalid Username OR Password";
                    return RedirectToAction(nameof(Login));
                }
            }catch (Exception ex) 
            {
                TempData["InvalidLogin"] = "Invalid Username OR Password";
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Login));
            }
            
            
        }
        // GET: users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTb = await _context.UserTb.FindAsync(id);
            if (userTb == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.RoleTb, "RoleId", "RoleName", userTb.RoleId);
            return View(userTb);
        }

        public IActionResult Logout()
        {
            // Clear user's session data
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("RoleName");

            // Redirect to the login page or any other page after logout
            return RedirectToAction(nameof(Login));
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password,Phone,Status,RoleId,CreatedAt")] UserTb userTb)
        {
            if (id != userTb.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTbExists(userTb.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.RoleTb, "RoleId", "RoleName", userTb.RoleId);
            return View(userTb);
        }

        // GET: users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTb = await _context.UserTb
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTb == null)
            {
                return NotFound();
            }

            return View(userTb);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTb = await _context.UserTb.FindAsync(id);
            _context.UserTb.Remove(userTb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTbExists(int id)
        {
            return _context.UserTb.Any(e => e.UserId == id);
        }
    }
}
