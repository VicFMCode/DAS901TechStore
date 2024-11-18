using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechStoreInventory.Data;
using TechStoreInventory.Models;

namespace TechStoreInventory.Controllers
{
    public class CelularesController : Controller
    {
        private readonly TechStoreContext _context;

        public CelularesController(TechStoreContext context)
        {
            _context = context;
        }

        // GET: Celulares
        public IActionResult Index(string searchString, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity)
        {
            // Consulta base para obtener todos los productos
            var celulares = _context.Celulares.AsQueryable();

            // Búsqueda por nombre o descripción
            if (!string.IsNullOrEmpty(searchString))
            {
                celulares = celulares.Where(c => c.Marca.Contains(searchString) || c.Modelo.Contains(searchString));
            }

            // Filtro por rango de precios
            if (minPrice.HasValue)
            {
                celulares = celulares.Where(c => c.Precio >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                celulares = celulares.Where(c => c.Precio <= maxPrice.Value);
            }

            // Filtro por rango de existencias
            if (minQuantity.HasValue)
            {
                celulares = celulares.Where(c => c.Existencias >= minQuantity.Value);
            }
            if (maxQuantity.HasValue)
            {
                celulares = celulares.Where(c => c.Existencias <= maxQuantity.Value);
            }

            // Pasar valores actuales de búsqueda y filtros a la vista
            ViewData["searchString"] = searchString;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["minQuantity"] = minQuantity;
            ViewData["maxQuantity"] = maxQuantity;

            return View(celulares.ToList());
        }

        // GET: Celulares/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = _context.Celulares.FirstOrDefault(m => m.ProductID == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // GET: Celulares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Celulares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductID,Marca,Modelo,Chip,Capacidad_in_GB,Camara,Color,Pantalla,Precio,Existencias")] Celular celular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(celular);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(celular);
        }

        // GET: Celulares/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = _context.Celulares.Find(id);
            if (celular == null)
            {
                return NotFound();
            }
            return View(celular);
        }

        // POST: Celulares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductID,Marca,Modelo,Chip,Capacidad_in_GB,Camara,Color,Pantalla,Precio,Existencias")] Celular celular)
        {
            if (id != celular.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(celular);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CelularExists(celular.ProductID))
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
            return View(celular);
        }

        // GET: Celulares/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = _context.Celulares.FirstOrDefault(m => m.ProductID == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // POST: Celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var celular = _context.Celulares.Find(id);
            _context.Celulares.Remove(celular);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CelularExists(int id)
        {
            return _context.Celulares.Any(e => e.ProductID == id);
        }
    }
}
