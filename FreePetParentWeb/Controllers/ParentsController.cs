using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreePetParentWeb.Data;
using FreePetParentWeb.Models;

namespace FreePetParentWeb.Controllers
{
    public class ParentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parents
        public async Task<IActionResult> Index()
        {
              return _context.Parent != null ? 
                          View(await _context.Parent.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Parent'  is null.");
        }
        
        // GET: Parents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: Parents/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Parents/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST: Parents/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {

            return View("Index", await _context.Parent.Where(p => p.PostalCode.Contains(SearchPhrase)).ToListAsync());
         
        }
        // POST: Parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentName,StreetAddress,City,State,Country,PostalCode,Phone,PetDescription,PetRequirements")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parent);
        }

        // GET: Parents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentName,StreetAddress,City,State,Country,PostalCode,Phone,PetDescription,PetRequirements")] Parent parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.Id))
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
            return View(parent);
        }

        // GET: Parents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parent'  is null.");
            }
            var parent = await _context.Parent.FindAsync(id);
            if (parent != null)
            {
                _context.Parent.Remove(parent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int id)
        {
          return (_context.Parent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
