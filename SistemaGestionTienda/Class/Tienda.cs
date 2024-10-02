using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTienda.Class
{
    public class Tienda
    {
        private List<Producto> inventario;

        public Tienda()
        {
            inventario = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            inventario.Add(producto);
        }

        public Producto BuscarProducto(string nombre)
        {
            foreach (var producto in inventario)
            {
                if (producto.Nombre == nombre)
                {
                    return producto;
                }
            }
            return null;
        }

        public bool EliminarProducto(string nombre)
        {
            var producto = BuscarProducto(nombre);
            if (producto != null)
            {
                inventario.Remove(producto);
                return true;
            }
            return false;
        }
    }
}
