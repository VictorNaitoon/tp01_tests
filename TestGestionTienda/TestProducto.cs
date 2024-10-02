using SistemaGestionTienda.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestGestionTienda
{
    public class TestProducto
    {
        [Fact]
        public void CrearProducto_PropiedadesAsignadasCorrectamente()
        {
            var producto = new Producto("Rasta", 5.5m, "Alfajor");
            Assert.Equal("Rasta", producto.Nombre);
            Assert.Equal(5.5m, producto.Precio);
            Assert.Equal("Alfajor", producto.Categoria);
        }
    }
}
