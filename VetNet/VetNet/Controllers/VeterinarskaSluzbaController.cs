using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetNet.Data;
using VetNet.Models;

namespace VetNet.Controllers
{
    public class VeterinarskaSluzbaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinarskaSluzbaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VeterinarskaSluzba
        public async Task<IActionResult> Index()
        {
            return View(await _context.VeterinarskaSluzba.ToListAsync());
        }

        // GET: VeterinarskaSluzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarskaSluzba = await _context.VeterinarskaSluzba
                .FirstOrDefaultAsync(m => m.id == id);
            if (veterinarskaSluzba == null)
            {
                return NotFound();
            }

            return View(veterinarskaSluzba);
        }

        // GET: VeterinarskaSluzba/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VeterinarskaSluzba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,naziv,adresa,brojTelefona,email")] VeterinarskaSluzba veterinarskaSluzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinarskaSluzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinarskaSluzba);
        }

        // GET: VeterinarskaSluzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarskaSluzba = await _context.VeterinarskaSluzba.FindAsync(id);
            if (veterinarskaSluzba == null)
            {
                return NotFound();
            }
            return View(veterinarskaSluzba);
        }

        // POST: VeterinarskaSluzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,adresa,brojTelefona,email")] VeterinarskaSluzba veterinarskaSluzba)
        {
            if (id != veterinarskaSluzba.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinarskaSluzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarskaSluzbaExists(veterinarskaSluzba.id))
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
            return View(veterinarskaSluzba);
        }

        // GET: VeterinarskaSluzba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarskaSluzba = await _context.VeterinarskaSluzba
                .FirstOrDefaultAsync(m => m.id == id);
            if (veterinarskaSluzba == null)
            {
                return NotFound();
            }

            return View(veterinarskaSluzba);
        }

        // POST: VeterinarskaSluzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarskaSluzba = await _context.VeterinarskaSluzba.FindAsync(id);
            if (veterinarskaSluzba != null)
            {
                _context.VeterinarskaSluzba.Remove(veterinarskaSluzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarskaSluzbaExists(int id)
        {
            return _context.VeterinarskaSluzba.Any(e => e.id == id);
        }
    }
}
