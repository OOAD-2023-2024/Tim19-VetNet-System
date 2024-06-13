using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetNet.Data;
using VetNet.Models;

namespace VetNet.Controllers
{
    public class PoslovnicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoslovnicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Poslovnica
        public async Task<IActionResult> Index()
        {
            return View(await _context.Poslovnica.ToListAsync());
        }

        // GET: Poslovnica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poslovnica = await _context.Poslovnica
                .FirstOrDefaultAsync(m => m.id == id);
            if (poslovnica == null)
            {
                return NotFound();
            }

            return View(poslovnica);
        }

        // GET: Poslovnica/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poslovnica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("id,naziv,adresa,brojTelefona,email")] Poslovnica poslovnica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poslovnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poslovnica);
        }

        // GET: Poslovnica/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poslovnica = await _context.Poslovnica.FindAsync(id);
            if (poslovnica == null)
            {
                return NotFound();
            }
            return View(poslovnica);
        }

        // POST: Poslovnica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,adresa,brojTelefona,email")] Poslovnica poslovnica)
        {
            if (id != poslovnica.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poslovnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoslovnicaExists(poslovnica.id))
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
            return View(poslovnica);
        }

        // GET: Poslovnica/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poslovnica = await _context.Poslovnica
                .FirstOrDefaultAsync(m => m.id == id);
            if (poslovnica == null)
            {
                return NotFound();
            }

            return View(poslovnica);
        }

        // POST: Poslovnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poslovnica = await _context.Poslovnica.FindAsync(id);
            if (poslovnica != null)
            {
                _context.Poslovnica.Remove(poslovnica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoslovnicaExists(int id)
        {
            return _context.Poslovnica.Any(e => e.id == id);
        }
    }
}
