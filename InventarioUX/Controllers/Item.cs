using InventarioUX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUX.Controllers
{
    public class Item
    {
            public PRODUCTOS producto = new PRODUCTOS();

            public PRODUCTOS Producto
            {
                get { return producto; }
                set { producto = value; }
            }

            public int cantidad;
            public int Cantidad
            {
                get { return cantidad; }
                set { cantidad = value; }
            }


            public Item()
            {

            }

            public Item(PRODUCTOS Producto, int Cantidad)
            {
                this.Producto = producto;
                this.Cantidad = cantidad;
            }
        }
}