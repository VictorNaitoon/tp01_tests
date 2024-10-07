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
        
        [Fact]
        public void CalcularTotalCarrito_ConDescuentos_DevuelveTotalCorrecto()
        {
            //Agrego más productos y aplico descuentos en estos productos.
            _tienda.AgregarProducto(new Producto("Chocolate", 20.0m, "Dulce"));
            _tienda.AgregarProducto(new Producto("Galletas", 15.0m, "Snack"));
            _tienda.AplicarDescuento("Galletas", 10); // 15.0m - 10% = 13.5m
            _tienda.AplicarDescuento("Chocolate", 20); // 20.0m - 20% = 12.0m
            //Creo un carrito de compras y calculo el total de la compra
            var carrito = new List<string> { "Rasta", "Pepsi", "Chocolate", "Galletas" };
            var total = _tienda.calcular_total_carrito(carrito);

            Assert.Equal(44m, total);
        }
        
        [Fact]
        public void CalcularTotalCarrito_ConProductoInexistente()
        {
            //Creo un carrito con un producto que no tenemos en la tienda y calculamos el total de la compra
            var carrito = new List<string> { "Rasta", "Naranja", "Pepsi" };
            var total = _tienda.calcular_total_carrito(carrito);

            Assert.Equal(15.5m, total);
        }
        
        [Fact]
        public void FlujoCompleto()
        {
            //Agrego nuevos productos a la tienda
            _tienda.AgregarProducto(new Producto("Chocolate", 20.0m, "Dulce"));
            _tienda.AgregarProducto(new Producto("Galletas", 15.0m, "Snack"));
            //Aplico descuentos algunos productos
            _tienda.AplicarDescuento("Rasta", 10); // 5.5m - 10% = 4.95m
            _tienda.AplicarDescuento("Chocolate", 20); // 20.0m - 20% = 16.0m
            //Actualizo el precio de un producto
            _tienda.ActualizarPrecio("Pepsi", 12.0m);
            //Elimino un producto de la tienda
            _tienda.EliminarProducto("Galletas");
            //Creo un carrito de compras
            var carrito = new List<string> { "Rasta", "Pepsi", "Chocolate", "Galletas" };
            //Calculo el monto total a pagar 
            var total = _tienda.calcular_total_carrito(carrito);

            //Verificar total (4.95m + 12.0m + 16.0m = 32.95m)
            Assert.Equal(32.95m, total);
        }
        
    }
}
