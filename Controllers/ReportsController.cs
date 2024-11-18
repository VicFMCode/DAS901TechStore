using Microsoft.AspNetCore.Mvc;
using TechStoreInventory.Data;
using System.Linq;

namespace TechStoreInventory.Controllers
{
    public class ReportsController : Controller
    {
        private readonly TechStoreContext _context;

        public ReportsController(TechStoreContext context)
        {
            _context = context;
        }
        public IActionResult Statistics()
        {
            var totalProducts = _context.Celulares.Sum(p => p.Existencias);
            var totalInventoryValue = _context.Celulares.Sum(p => p.Precio * p.Existencias);

            // Formatear el valor para la vista, pero mantener el número original para el gráfico
            ViewBag.TotalInventoryValue = totalInventoryValue.ToString("C2"); // Para mostrar en la vista con formato de moneda

            // Pasar el valor numérico al gráfico
            ViewBag.TotalInventoryValueNumeric = totalInventoryValue;

            ViewBag.TotalProducts = totalProducts;

            return View();
        }

        // Página principal de informes
        public IActionResult Index()
        {
            return View();
        }

        // Generar Informe: Productos con menor stock
        public IActionResult LowStockReport()
        {
            var lowStockProducts = _context.Celulares
                .Where(p => p.Existencias <= 50) // Cambia el valor de 10 según tu criterio
                .OrderBy(p => p.Existencias)
                .ToList();

            return View(lowStockProducts);
        }

        // Generar Informe: Productos más vendidos (simulado aquí)
        public IActionResult TopSellingReport()
        {
            // Simulación: suponiendo que "Existencias" indica productos vendidos
            var topSellingProducts = _context.Celulares
                .OrderByDescending(p => p.Existencias) // Cambia Existencias a tu columna real si tienes datos de ventas
                .Take(5)
                .ToList();

            return View(topSellingProducts);
        }
    }
}
