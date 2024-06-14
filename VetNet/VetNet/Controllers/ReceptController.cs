using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize(Roles = "Administrator, Veterinar, Apotekar")]
    public class ReceptController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReceptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recepts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recept.Include(r => r.Korisnik).Include(r => r.Ljubimac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recepts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept
                .Include(r => r.Korisnik)
                .Include(r => r.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recept == null)
            {
                return NotFound();
            }

            return View(recept);
        }

        // GET: Recepts/Create
        [Authorize(Roles = "Administrator, Veterinar")]
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime");
            return View();
        }

        // POST: Recepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Create([Bind("Id,datumVrijeme,lijek,doza,napomena,LjubimacId,KorisnikId")] Recept recept)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            recept.KorisnikId = userId;
            if (ModelState.IsValid)
            {
                _context.Add(recept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", recept.LjubimacId);
            return View(recept);
        }

        // GET: Recepts/Edit/5
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept.FindAsync(id);
            if (recept == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", recept.LjubimacId);
            return View(recept);
        }

        // POST: Recepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,datumVrijeme,lijek,doza,napomena,LjubimacId,KorisnikId")] Recept recept)
        {
            if (id != recept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptExists(recept.Id))
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
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "ime", recept.LjubimacId);
            return View(recept);
        }

        // GET: Recepts/Delete/5
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept
                .Include(r => r.Korisnik)
                .Include(r => r.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recept == null)
            {
                return NotFound();
            }

            return View(recept);
        }

        // POST: Recepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Veterinar")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recept = await _context.Recept.FindAsync(id);
            if (recept != null)
            {
                _context.Recept.Remove(recept);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptExists(int id)
        {
            return _context.Recept.Any(e => e.Id == id);
        }
    }
}
