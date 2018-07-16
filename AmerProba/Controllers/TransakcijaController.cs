using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmerProba.Data;
using Microsoft.AspNetCore.Identity;

namespace AmerProba.Controllers
{
    public class TransakcijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransakcijaController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transakcija
        public async Task<IActionResult> Index()
        {

            //var userId = int.Parse(_userManager.GetUserId(User));
            var userId = (_userManager.GetUserId(User));
            ViewBag.UserID = userId;
            List<Transakcija> TrLista =new List<Transakcija>();
            foreach (var tr in _context.Transakcije)
            {
                if(tr.KorisnikId==userId)
                    TrLista.Add(tr);
            }
            return View(TrLista);
            //return View(await _context.Transakcije.ToListAsync());
        }

        // GET: Transakcija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcije
                .FirstOrDefaultAsync(m => m.TransakcijaId == id);
            if (transakcija == null)
            {
                return NotFound();
            }

            return View(transakcija);
        }

        // GET: Transakcija/Create
        public IActionResult Create()
        {
            var userId = (_userManager.GetUserId(User));
            ViewBag.UserID = userId;

            var selectListaTipoviTransakcija = Enum.GetValues(typeof(TipTransakcije))
                .Cast<TipTransakcije>()
                .Select(tt => new SelectionListItem
                {
                    Value = ((int)tt).ToString(),
                    Text = tt.ToString()
                });
            ViewBag.TipTransakcije = new SelectList(selectListaTipoviTransakcija, "Value", "Text");
            //var selectListaSpisakKorisnika = new List<User>().Select(tt=> new SelectListItem
            //    {
            //        Value = tt.Id,
            //        Text = tt.UserName
            //    });
               
            //ViewBag.SpisakKorisnika = new SelectList(selectListaSpisakKorisnika, "Value", "Text");
            return View();
        }

        // POST: Transakcija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransakcijaId,Tip,Detalji,Iznos,Vrijeme,KorisnikId")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transakcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transakcija);
        }

        // GET: Transakcija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcije.FindAsync(id);
            if (transakcija == null)
            {
                return NotFound();
            }
            var selectListaTipoviTransakcija = Enum.GetValues(typeof(TipTransakcije))
                .Cast<TipTransakcije>()
                .Select(tt => new SelectionListItem
                {
                    Value = ((int)tt).ToString(),
                    Text = tt.ToString()
                });
            ViewBag.TipTransakcije = new SelectList(selectListaTipoviTransakcija, "Value", "Text");
            return View(transakcija);
        }

        // POST: Transakcija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransakcijaId,Tip,Detalji,Iznos,Vrijeme,KorisnikId")] Transakcija transakcija)
        {
            if (id != transakcija.TransakcijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transakcija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransakcijaExists(transakcija.TransakcijaId))
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
            return View(transakcija);
        }

        // GET: Transakcija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcije
                .FirstOrDefaultAsync(m => m.TransakcijaId == id);
            if (transakcija == null)
            {
                return NotFound();
            }

            return View(transakcija);
        }

        // POST: Transakcija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transakcija = await _context.Transakcije.FindAsync(id);
            _context.Transakcije.Remove(transakcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransakcijaExists(int id)
        {
            return _context.Transakcije.Any(e => e.TransakcijaId == id);
        }
    }
}
