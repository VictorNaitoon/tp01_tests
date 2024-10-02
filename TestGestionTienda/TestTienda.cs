using SistemaGestionTienda.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestGestionTienda
{
    public class TestTienda
    {
        [Fact]
        public void AgregarProducto_ProductoEstaEnInventario()
        {
            var tienda = new Tienda();
            var producto = new Producto("Rasta", 5.5m, "Alfajor");
            tienda.AgregarProducto(producto);
            Assert.Equal(producto, tienda.BuscarProducto("Rasta"));
        }

        [Fact]
        public void BuscarProducto_DevuelveProducto()
        {
            //Si el producto existe nos devuelve el producto mismo
            var tienda = new Tienda();
            var producto = new Producto("Rasta", 5.5m, "Alfajor");
            tienda.AgregarProducto(producto);
            var productoBuscado = tienda.BuscarProducto("Rasta");
            Assert.NotNull(productoBuscado);
            Assert.Equal("Rasta", productoBuscado.Nombre);
        }

        [Fact]
        public void BuscarProducto_LanzaExcepcion()
        {
            //Si el producto no existe nos lanza una excepción
            var tienda = new Tienda();
            var excepcion = Assert.Throws<Exception>(() => tienda.BuscarProducto("Pepsi"));
            Assert.Equal("El producto Pepsi no se encontró en la tienda.", excepcion.Message);
        }

        [Fact]
        public void EliminarProducto_EliminaCorrectamente()
        {
            //Si el producto existe elimina el producto
            var tienda = new Tienda();
            var producto = new Producto("Rasta", 5.5m, "Alfajor");
            tienda.AgregarProducto(producto);
            var resultado = tienda.EliminarProducto("Rasta");
            Assert.True(resultado);
            Assert.Throws<Exception>(() => tienda.BuscarProducto("Rasta"));
        }

        [Fact]
        public void EliminarProducto_LanzaExcepcion()
        {
            //Si el producto no existe nos tira una excepción
            var tienda = new Tienda();
            var excepcion = Assert.Throws<Exception>(() => tienda.EliminarProducto("Pepsi"));
            Assert.Equal("No se puede eliminar el producto Pepsi porque no existe en la tienda.",excepcion.Message);
        }

        [Fact]
        public void ActualizarPrecio_PrecioActualizado()
        {
            //Si el producto existe actualiza el precio
            var tienda = new Tienda();
            var producto = new Producto("Rasta", 5.5m, "Alfajor");
            tienda.AgregarProducto(producto);
            tienda.ActualizarPrecio("Rasta", 8.0m);
            var productoBuscado = tienda.BuscarProducto("Rasta");
            Assert.Equal(8.0m, productoBuscado.Precio);
        }

        [Fact]
        public void ActualizarPrecio_LanzaExcepcion()
        {
            // Si el producto no existe nos devuelve una excepcion
            var tienda = new Tienda();
            var exception = Assert.Throws<Exception>(() => tienda.ActualizarPrecio("Pepsi", 12.0m));
            Assert.Equal("No se puede actualizar el precio del producto Pepsi porque no se encontró en la tienda.", exception.Message);
        }
    }
}
