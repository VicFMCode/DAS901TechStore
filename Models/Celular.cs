using System.ComponentModel.DataAnnotations;

namespace TechStoreInventory.Models
{
    public class Celular
    {
        [Key]
        public int ProductID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Chip { get; set; }
        public int Capacidad_in_GB { get; set; }
        public string Camara { get; set; }
        public string Color { get; set; }
        public string Pantalla { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
    }
}
