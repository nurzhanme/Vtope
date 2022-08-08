﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vtope.Models;

namespace Vtope.Controllers
{
    public class InstaAccountsController : Controller
    {
        private readonly VtopeDbContext _context;

        public InstaAccountsController(VtopeDbContext context)
        {
            _context = context;
        }

        // GET: InstaAccounts
        public async Task<IActionResult> Index()
        {
              return _context.InstaAccounts != null ? 
                          View(await _context.InstaAccounts.ToListAsync()) :
                          Problem("Entity set 'VtopeDbContext.InstaAccounts'  is null.");
        }

        // GET: InstaAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InstaAccounts == null)
            {
                return NotFound();
            }

            var instaAccount = await _context.InstaAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instaAccount == null)
            {
                return NotFound();
            }

            return View(instaAccount);
        }

        // GET: InstaAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstaAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,SessionData,IsUtil")] InstaAccount instaAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instaAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instaAccount);
        }

        // GET: InstaAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InstaAccounts == null)
            {
                return NotFound();
            }

            var instaAccount = await _context.InstaAccounts.FindAsync(id);
            if (instaAccount == null)
            {
                return NotFound();
            }
            return View(instaAccount);
        }

        // POST: InstaAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,SessionData,IsUtil")] InstaAccount instaAccount)
        {
            if (id != instaAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instaAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstaAccountExists(instaAccount.Id))
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
            return View(instaAccount);
        }

        // GET: InstaAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InstaAccounts == null)
            {
                return NotFound();
            }

            var instaAccount = await _context.InstaAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instaAccount == null)
            {
                return NotFound();
            }

            return View(instaAccount);
        }

        // POST: InstaAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InstaAccounts == null)
            {
                return Problem("Entity set 'VtopeDbContext.InstaAccounts'  is null.");
            }
            var instaAccount = await _context.InstaAccounts.FindAsync(id);
            if (instaAccount != null)
            {
                _context.InstaAccounts.Remove(instaAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstaAccountExists(int id)
        {
          return (_context.InstaAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}