using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven5.Models;

namespace BookShelfHaven5.Controllers
{
    public class AddAdminController : Controller
    {
        private readonly BookShelfHavenContext _context;

        public AddAdminController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: AddAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }

        // GET: AddAdmin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: AddAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Email,PasswordHash,FailedLoginAttempts")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(admin.PasswordHash);
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "AddAdmin");// CHANGING THE LINK TO REDIRECT TO THE CREATE PAGE OF THE MODULE INFORMAITON
            }
            return View(admin);
          
        }

        // GET: AddAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: AddAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,FirstName,LastName,Email,PasswordHash,FailedLoginAttempts")] Admin admin)
        {
            if (id != admin.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Username))
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
            return View(admin);
        }

        // GET: AddAdmin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: AddAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.Username == id);
        }
    }
}
