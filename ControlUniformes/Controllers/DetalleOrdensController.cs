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
    public class DetalleOrdensController : Controller
    {
        private readonly masterContext _context;

        public DetalleOrdensController(masterContext context)
        {
            _context = context;
        }

        // GET: DetalleOrdens
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.DetalleOrdens.Include(d => d.IdOrdenNavigation);
            return View(await masterContext.ToListAsync());
        }

        // GET: DetalleOrdens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleOrdens == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrdens
                .Include(d => d.IdOrdenNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Create
        public IActionResult Create()
        {
            ViewData["IdOrden"] = new SelectList(_context.OrdenesProduccions, "IdOrden", "IdOrden");
            return View();
        }

        // POST: DetalleOrdens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalle,IdOrden,Articulo,Descripcion,TotalPiezas,Tallas,PrecioUnitario,ImporteTotal")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleOrden);
                await _context.SaveChangesAsync();
                var orden = _context.OrdenesProduccions.FirstOrDefault(y => y.IdOrden == detalleOrden.IdOrden);
                if (orden != null) 
                {
                    var Total = orden.Total != null ? orden.Total + detalleOrden.ImporteTotal : detalleOrden.ImporteTotal;
                    orden.Total = Total;
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrden"] = new SelectList(_context.OrdenesProduccions, "IdOrden", "IdOrden", detalleOrden.IdOrden);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleOrdens == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrdens.FindAsync(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }
            ViewData["IdOrden"] = new SelectList(_context.OrdenesProduccions, "IdOrden", "IdOrden", detalleOrden.IdOrden);
            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalle,IdOrden,Articulo,Descripcion,TotalPiezas,Tallas,PrecioUnitario,ImporteTotal")] DetalleOrden detalleOrden)
        {
            if (id != detalleOrden.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleOrden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleOrdenExists(detalleOrden.IdDetalle))
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
            ViewData["IdOrden"] = new SelectList(_context.OrdenesProduccions, "IdOrden", "IdOrden", detalleOrden.IdOrden);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleOrdens == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrdens
                .Include(d => d.IdOrdenNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleOrdens == null)
            {
                return Problem("Entity set 'masterContext.DetalleOrdens'  is null.");
            }
            var detalleOrden = await _context.DetalleOrdens.FindAsync(id);
            if (detalleOrden != null)
            {
                _context.DetalleOrdens.Remove(detalleOrden);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleOrdenExists(int id)
        {
          return (_context.DetalleOrdens?.Any(e => e.IdDetalle == id)).GetValueOrDefault();
        }
    }
}
