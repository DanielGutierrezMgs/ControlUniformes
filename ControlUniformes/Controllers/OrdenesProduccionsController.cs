using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlUniformes.Models.Entities;

namespace ControlUniformes.Controllers
{
    public class OrdenesProduccionsController : Controller
    {
        private readonly masterContext _context;

        public OrdenesProduccionsController(masterContext context)
        {
            _context = context;
        }

        // GET: OrdenesProduccions
        public async Task<IActionResult> Index()
        {
              return _context.OrdenesProduccions != null ? 
                          View(await _context.OrdenesProduccions.ToListAsync()) :
                          Problem("Entity set 'masterContext.OrdenesProduccions'  is null.");
        }

        // GET: OrdenesProduccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenesProduccions == null)
            {
                return NotFound();
            }

            var ordenesProduccion = await _context.OrdenesProduccions
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenesProduccion == null)
            {
                return NotFound();
            }

            return View(ordenesProduccion);
        }

        // GET: OrdenesProduccions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdenesProduccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrden,Pedido,Fecha,Maquilero,OrdenProduccion,Total")] OrdenesProduccion ordenesProduccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenesProduccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordenesProduccion);
        }

        // GET: OrdenesProduccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenesProduccions == null)
            {
                return NotFound();
            }

            var ordenesProduccion = await _context.OrdenesProduccions.FindAsync(id);
            if (ordenesProduccion == null)
            {
                return NotFound();
            }
            return View(ordenesProduccion);
        }

        // POST: OrdenesProduccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrden,Pedido,Fecha,Maquilero,OrdenProduccion,Total")] OrdenesProduccion ordenesProduccion)
        {
            if (id != ordenesProduccion.IdOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenesProduccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenesProduccionExists(ordenesProduccion.IdOrden))
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
            return View(ordenesProduccion);
        }

        // GET: OrdenesProduccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenesProduccions == null)
            {
                return NotFound();
            }

            var ordenesProduccion = await _context.OrdenesProduccions
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenesProduccion == null)
            {
                return NotFound();
            }

            return View(ordenesProduccion);
        }

        // POST: OrdenesProduccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenesProduccions == null)
            {
                return Problem("Entity set 'masterContext.OrdenesProduccions'  is null.");
            }
            var ordenesProduccion = await _context.OrdenesProduccions.FindAsync(id);
            if (ordenesProduccion != null)
            {
                _context.OrdenesProduccions.Remove(ordenesProduccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenesProduccionExists(int id)
        {
          return (_context.OrdenesProduccions?.Any(e => e.IdOrden == id)).GetValueOrDefault();
        }
    }
}
