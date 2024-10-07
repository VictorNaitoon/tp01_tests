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

        public virtual void AgregarProducto(Producto producto)
        {
            inventario.Add(producto);
        }

        public virtual Producto BuscarProducto(string nombre)
        {
            foreach (var producto in inventario)
            {
                if (producto.Nombre == nombre)
                {
                    return producto;
                }
            }
            throw new Exception($"El producto {nombre} no se encontró en la tienda.");
        }

        public virtual bool EliminarProducto(string nombre)
        {
            try
            {
                var producto = BuscarProducto(nombre);
                inventario.Remove(producto);
                return true;
            }
            catch (Exception)
            {
                throw new Exception($"No se puede eliminar el producto {nombre} porque no existe en la tienda.");
            }
        }

        public virtual void ActualizarPrecio(string nombre, decimal nuevoPrecio)
        {
            try
            {
                var producto = BuscarProducto(nombre);
                producto.Precio = Math.Round(nuevoPrecio, 2);
            }
            catch (Exception)
            {
                throw new Exception($"No se puede actualizar el precio del producto {nombre} porque no se encontró en la tienda.");
            }
        }

        public virtual void AplicarDescuento(string nombre, decimal porcentajeDescuento)
        {
            try
            {
                var producto = BuscarProducto(nombre);
                var factor = 1 - (porcentajeDescuento / 100);
                var nuevoPrecio = producto.Precio * factor;
                ActualizarPrecio(nombre, Math.Round(nuevoPrecio, 2));
            }
            catch (Exception)
            {
                throw new Exception($"No se puede aplicar el descuento al producto {nombre} porque no existe.");
            }
        }

        public virtual decimal calcular_total_carrito(List<string> productos)
        {
            decimal total = 0;
            foreach (var nombre in productos)
            {
                try
                {
                    var producto = BuscarProducto(nombre);
                    total += producto.Precio;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return total;
        }
    }
}
