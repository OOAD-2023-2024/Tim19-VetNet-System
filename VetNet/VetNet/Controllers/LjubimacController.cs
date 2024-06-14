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
    [Authorize(Roles = "Administrator, Veterinar, Apotekar")]
    public class LjubimacController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LjubimacController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ljubimac
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ljubimac.Include(l => l.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ljubimac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // GET: Ljubimac/Create
        [Authorize(Roles = "Administrator, Veterinar")]
        public IActionResult Create()
        {
            ViewData["rasa"] = new SelectList(Enum.GetValues(typeof(Ljubimac.Rasa)).Cast<Ljubimac.Rasa>());
            ViewData["KorisnikId"] = new SelectList(_context.Users.Select(u => new
            {
                Id = u.Id,
                ime = u.ime + " " + u.prezime
            }), "Id", "ime");
            ViewData["spol"] = new SelectList(Enum.GetValues(typeof(Spol)).Cast<Spol>());
            return View();
        }

        // POST: Ljubimac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Create([Bind("Id,ime,datumRodjenja,slika,rasa,spol,qrCode,KorisnikId")] Ljubimac ljubimac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ljubimac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["rasa"] = new SelectList(Enum.GetValues(typeof(Ljubimac.Rasa)).Cast<Ljubimac.Rasa>());
            ViewData["KorisnikId"] = new SelectList(_context.Users.Select(u => new
            {
                Id = u.Id,
                ime = u.ime + " " + u.prezime
            }), "Id", "ime", ljubimac.KorisnikId);
            ViewData["spol"] = new SelectList(Enum.GetValues(typeof(Spol)).Cast<Spol>());
            return View(ljubimac);
        }

        // GET: Ljubimac/Edit/5
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac.FindAsync(id);
            if (ljubimac == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users.Select(u => new
            {
                Id = u.Id,
                ime = u.ime + " " + u.prezime
            }), "Id", "ime", ljubimac.KorisnikId);
            return View(ljubimac);
        }

        // POST: Ljubimac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ime,datumRodjenja,slika,rasa,spol,qrCode,KorisnikId")] Ljubimac ljubimac)
        {
            if (id != ljubimac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ljubimac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LjubimacExists(ljubimac.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Users.Select(u => new
            {
                Id = u.Id,
                ime = u.ime + " " + u.prezime
            }), "Id", "ime", ljubimac.KorisnikId);
            return View(ljubimac);
        }

        // GET: Ljubimac/Delete/5
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // POST: Ljubimac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ljubimac = await _context.Ljubimac.FindAsync(id);
            if (ljubimac != null)
            {
                _context.Ljubimac.Remove(ljubimac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LjubimacExists(int id)
        {
            return _context.Ljubimac.Any(e => e.Id == id);
        }
    }
}
