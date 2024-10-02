using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTienda.Class
{
    public class TiendaFixture 
    {
        public Tienda TiendaConProductos { get; set; }

        public TiendaFixture()
        {
            TiendaConProductos = new Tienda();
            TiendaConProductos.AgregarProducto(new Producto("Rasta", 5.5m, "Alfajor"));
            TiendaConProductos.AgregarProducto(new Producto("Pepsi", 10.0m, "Bebida"));
        }
    }
}
