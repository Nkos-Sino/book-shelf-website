using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven5.Models;
using Microsoft.AspNetCore.Localization;

namespace BookShelfHaven5.Controllers
{
    public class LoginController : Controller
    {
        private readonly BookShelfHavenContext _context;

        public LoginController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Email,PasswordHash,FailedLoginAttempts")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
       
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database based on the provided username
                var userInDb = await _context.Customers.SingleOrDefaultAsync(u => u.Username == username);
                var AdminInDb = await _context.Admin.SingleOrDefaultAsync(u => u.Username == username);

                if (AdminInDb != null && AdminInDb != null && BCrypt.Net.BCrypt.Verify(password, AdminInDb.PasswordHash))
                {
                    // Password matches, user is logged in
                    // For simplicity, you can set a session variable or authentication cookie here
                    //HttpContext.Session.SetString("UserId", userInDb.Id); // Example of setting session variable
                    HttpContext.Session.SetString("Username", AdminInDb.Username);
                    // Redirect the user to the dashboard or home page
                    return RedirectToAction("Index", "AddAdmin");
                }
                else


                if (userInDb != null && userInDb != null && BCrypt.Net.BCrypt.Verify(password, userInDb.PasswordHash))
                {
                    // Password matches, user is logged in
                    // For simplicity, you can set a session variable or authentication cookie here
                    //HttpContext.Session.SetString("UserId", userInDb.Id); // Example of setting session variable
                    HttpContext.Session.SetString("Username", userInDb.Username);
                    // Redirect the user to the dashboard or home page
                    return RedirectToAction("Index", "HomePage");
                }


                // Model is invalid, return to the login view with validation errors
            }
            ModelState.AddModelError("Username", "Incorrect username, please try again");
            ModelState.AddModelError("PasswordHash", "Incorrect password please try again");
            //ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View("Create");
            }
        
        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,FirstName,LastName,Email,PasswordHash,FailedLoginAttempts")] Customer customer)
        {
            if (id != customer.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Username))
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
            return View(customer);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.Username == id);
        }
    }
}
