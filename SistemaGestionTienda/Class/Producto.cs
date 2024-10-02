using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTienda.Class
{
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

        public Producto(string nombre, decimal precio, string categoria)
        {
            Nombre = nombre;
            Precio = precio;
            Categoria = categoria;
        }
    }
}
