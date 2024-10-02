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
        public void BuscarProducto_DevuelveNull()
        {
            //Si el producto no existe nos devuelve null
            var tienda = new Tienda();
            var productoBuscado = tienda.BuscarProducto("Pepsi");
            Assert.Null(productoBuscado);
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
            Assert.Null(tienda.BuscarProducto("Rasta"));
        }

        [Fact]
        public void EliminarProducto_DevuelveFalse()
        {
            //Si el producto no existe devuelve false
            var tienda = new Tienda();
            var resultado = tienda.EliminarProducto("Pepsi");
            Assert.False(resultado);
        }
    }
}
