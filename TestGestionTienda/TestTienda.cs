using SistemaGestionTienda.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace TestGestionTienda
{
    public class TestTienda : IClassFixture<TiendaFixture>
    {
        private readonly Tienda _tienda;

        public TestTienda(TiendaFixture _tiendaFixture)
        {
            _tienda = _tiendaFixture.TiendaConProductos;
        }

        [Fact]
        public void AgregarProducto_ProductoEstaEnInventario()
        {
            var nuevoProducto = new Producto("Coca-Cola", 15.0m, "Bebida");
            _tienda.AgregarProducto(nuevoProducto);
            Assert.Equal(nuevoProducto, _tienda.BuscarProducto("Coca-Cola"));
        }

        [Fact]
        public void BuscarProducto_DevuelveProducto()
        {
            //Si el producto existe nos devuelve el producto mismo
            var productoBuscado = _tienda.BuscarProducto("Rasta");
            Assert.NotNull(productoBuscado);
            Assert.Equal("Rasta", productoBuscado.Nombre);
        }

        [Fact]
        public void BuscarProducto_LanzaExcepcion()
        {
            //Si el producto no existe nos lanza una excepción
            var excepcion = Assert.Throws<Exception>(() => _tienda.BuscarProducto("Naranja"));
            Assert.Equal("El producto Naranja no se encontró en la tienda.", excepcion.Message);
        }

        [Fact]
        public void EliminarProducto_EliminaCorrectamente()
        {
            //Si el producto existe elimina el producto
            var productoEliminar = _tienda.EliminarProducto("Rasta");
            Assert.True(productoEliminar);
            Assert.Throws<Exception>(() => _tienda.BuscarProducto("Rasta"));
        }

        [Fact]
        public void EliminarProducto_LanzaExcepcion()
        {
            //Si el producto no existe nos tira una excepción
            var excepcion = Assert.Throws<Exception>(() => _tienda.EliminarProducto("Naranja"));
            Assert.Equal("No se puede eliminar el producto Naranja porque no existe en la tienda.",excepcion.Message);
        }

        [Fact]
        public void ActualizarPrecio_PrecioActualizado()
        {
            //Si el producto existe actualiza el precio
            _tienda.ActualizarPrecio("Pepsi", 12.0m);
            var producto = _tienda.BuscarProducto("Pepsi");
            Assert.Equal(12.0m, producto.Precio);
        }

        [Fact]
        public void ActualizarPrecio_LanzaExcepcion()
        {
            // Si el producto no existe nos devuelve una excepcion
            var exception = Assert.Throws<Exception>(() => _tienda.ActualizarPrecio("Naranja", 12.0m));
            Assert.Equal("No se puede actualizar el precio del producto Naranja porque no se encontró en la tienda.", exception.Message);
        }

        [Fact]
        public void AplicarDescuento_Correctamente()
        {
            _tienda.AplicarDescuento("Pepsi", 10);
            var producto = _tienda.BuscarProducto("Pepsi");
            Assert.Equal(9.0m, producto.Precio);
        }
    }
}
