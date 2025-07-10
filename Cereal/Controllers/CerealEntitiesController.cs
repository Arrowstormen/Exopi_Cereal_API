using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cereal.Data;
using Cereal.Models;
using Cereal.Authentication;

namespace Cereal.Controllers
{
    public class CerealEntitiesController : Controller
    {
        private readonly CerealContext _context;

        public CerealEntitiesController(CerealContext context)
        {
            _context = context;
        }

        // GET: CerealEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cereals.ToListAsync());
        }

        // GET: CerealEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cerealEntity = await _context.Cereals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cerealEntity == null)
            {
                return NotFound();
            }

            return View(cerealEntity);
        }

        // GET: CerealEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CerealEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, BasicAuthorization]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Manufacturer,Type,Calories,Protein,Fat,Sodium,Fiber,Carbo,Sugars,Potass,Vitamins,Shelf,Weight,Cups,Rating")] CerealEntity cerealEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cerealEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cerealEntity);
        }

        // GET: CerealEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cerealEntity = await _context.Cereals.FindAsync(id);
            if (cerealEntity == null)
            {
                return NotFound();
            }
            return View(cerealEntity);
        }

        // POST: CerealEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, BasicAuthorization]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Manufacturer,Type,Calories,Protein,Fat,Sodium,Fiber,Carbo,Sugars,Potass,Vitamins,Shelf,Weight,Cups,Rating")] CerealEntity cerealEntity)
        {
            if (id != cerealEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cerealEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CerealEntityExists(cerealEntity.Id))
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
            return View(cerealEntity);
        }

        // GET: CerealEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cerealEntity = await _context.Cereals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cerealEntity == null)
            {
                return NotFound();
            }

            return View(cerealEntity);
        }

        // POST: CerealEntities/Delete/5
        [HttpPost, BasicAuthorization, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var cerealEntity = await _context.Cereals.FindAsync(id);
            if (cerealEntity != null)
            {
                _context.Cereals.Remove(cerealEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CerealEntityExists(int? id)
        {
            return _context.Cereals.Any(e => e.Id == id);
        }
    }
}
