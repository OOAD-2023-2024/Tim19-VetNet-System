using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetNet.Data;
using VetNet.Models;

namespace VetNet.Controllers
{
    [Authorize(Roles = "Administrator, Veterinar")]
    public class PregledController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PregledController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pregleds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pregled.Include(p => p.Korisnik).Include(p => p.Ljubimac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pregleds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregled = await _context.Pregled
                .Include(p => p.Korisnik)
                .Include(p => p.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregled == null)
            {
                return NotFound();
            }

            return View(pregled);
        }

        // GET: Pregleds/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime");
            return View();
        }

        // POST: Pregleds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,datumVrijeme,razlog,postupak,dijagnoza,napomena,terapija,LjubimacId,KorisnikId")] Pregled pregled)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            pregled.KorisnikId = userId;
            if (ModelState.IsValid)
            {
                _context.Add(pregled);
                await _context.SaveChangesAsync();
                if (pregled.terapija)
                {
                    return RedirectToAction("CreateForLjubimac", "Recept", new { id = pregled.LjubimacId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", pregled.LjubimacId);
            return View(pregled);
        }

        
        [HttpGet("Pregleds/Create/{id}")]
        public IActionResult CreateForLjubimac(int id)
        {
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", id);
            return View();
        }


        [HttpPost("Pregleds/Create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForLjubimac([Bind("datumVrijeme,razlog,postupak,dijagnoza,napomena,terapija,LjubimacId,KorisnikId")] Pregled pregled)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            pregled.KorisnikId = userId;
            if (ModelState.IsValid)
            {
                _context.Add(pregled);
                await _context.SaveChangesAsync();
                if (pregled.terapija)
                {
                    return RedirectToAction("CreateForLjubimac", "Recept", new { id = pregled.LjubimacId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", pregled.LjubimacId);
            return View(pregled);
        }

        // GET: Pregleds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregled = await _context.Pregled.FindAsync(id);
            if (pregled == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", pregled.LjubimacId);
            return View(pregled);
        }

        // POST: Pregleds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,datumVrijeme,razlog,postupak,dijagnoza,napomena,terapija,LjubimacId,KorisnikId")] Pregled pregled)
        {
            if (id != pregled.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pregled);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PregledExists(pregled.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", pregled.LjubimacId);
            return View(pregled);
        }

        // GET: Pregleds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregled = await _context.Pregled
                .Include(p => p.Korisnik)
                .Include(p => p.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregled == null)
            {
                return NotFound();
            }

            return View(pregled);
        }

        // POST: Pregleds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pregled = await _context.Pregled.FindAsync(id);
            if (pregled != null)
            {
                _context.Pregled.Remove(pregled);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PregledExists(int id)
        {
            return _context.Pregled.Any(e => e.Id == id);
        }
    }
}
