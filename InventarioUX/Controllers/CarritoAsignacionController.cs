using InventarioUX.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioUX.Controllers
{
    public class CarritoAsignacionController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectProducto(FormCollection fc)
        {
            return View();
        }

        public ActionResult codigobarras(FormCollection fc)
        {
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();

            string[] cantidad = fc.GetValues("codigobarras");
            int codigobarras = Convert.ToInt32(cantidad[0]);

            var command = new SqlCommand("SELECT ID FROM dbo.PRODUCTOS WHERE CODIGOBARRAS='" + codigobarras + "'", con);
            int id = (int)(command.ExecuteScalar());

            return RedirectToAction("Agregar", new { id = id });
        }

        public ActionResult Carrito()
        {
            return View();
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count(); i++)
            {
                if (cart[i].Producto.ID == id)
                    return i;

            }
            return -1;
        }

        public ActionResult Eliminar(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Carrito");
        }

        public ActionResult Agregar(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var wea = db.PRODUCTOS.Find(id);
                Item wea2 = new Item();
                wea2.Producto = wea;
                wea2.Cantidad = 1;
                cart.Add(wea2);
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    var wea = db.PRODUCTOS.Find(id);
                    Item wea2 = new Item();
                    wea2.Producto = wea;
                    wea2.Cantidad = 1;
                    cart.Add(wea2);
                }
                else
                {
                    cart[index].Cantidad++;
                }

                Session["cart"] = cart;
            }
            return View("Carrito");
        }

        public ActionResult CheckOut()
        {
            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "DEPARTAMENTOID")] ASIGNACIONES asignaciones)
        {
            Session["Departamento"] = asignaciones.DEPARTAMENTOID;
            return View("Carrito");
        }

        public ActionResult Registrar()
        {
            ASIGNACIONES asignaciones = new ASIGNACIONES();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            Item i = new Item();
            List<Item> cart = (List<Item>)Session["cart"];
            asignaciones.EMPLEADOID = int.Parse(Session["ID"].ToString());
            int departamentoid = int.Parse(Session["Departamento"].ToString());
            asignaciones.DEPARTAMENTOID = departamentoid;
            asignaciones.Fecha = DateTime.Now;
            db.ASIGNACIONES.Add(asignaciones);
            db.SaveChanges();

            foreach (Item item in cart)
            {
                ASIGNACIONES_LISTA asignaciones_lista = new ASIGNACIONES_LISTA();
                asignaciones_lista.PRODUCTOID = item.Producto.ID;
                asignaciones_lista.PRECIO = item.Producto.PRECIO;
                asignaciones_lista.ASIGNACIONID = asignaciones.ID;
                db.ASIGNACIONES_LISTA.Add(asignaciones_lista);
                db.SaveChanges();
                SqlCommand command2 = new SqlCommand("SELECT NOMBRE FROM DEPARTAMENTOS WHERE ID = " + departamentoid + "", con);
                string departamentonombre = (string)(command2.ExecuteScalar());
                SqlCommand command = new SqlCommand("UPDATE PRODUCTOS SET UBICACION = '" + departamentonombre + "' WHERE ID = " + item.Producto.ID + "", con);
                command.ExecuteNonQuery();
            }
            Session.Remove("cart");
            Session.Remove("Departamento");
            return View("OrderSaved");
        }

        public ActionResult Cancelar()
        {
            return View("");
        }

        //public ActionResult Existencias()
        //{
        //    List<Item> cart = (List<Item>)Session["cart"];
        //    var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
        //    con.Open();
        //    int cont = 0;
        //    foreach (Item item in cart)
        //    {
        //        var command2 = new SqlCommand("SELECT CANTIDAD FROM dbo.ALMACENs WHERE PRODUCTOSID=" + item.Producto.ID + "", con);
        //        int existencia = (int)(command2.ExecuteScalar());
        //        if (item.Cantidad > existencia)
        //        {
        //            cont++;
        //        }
        //    }
        //    if (cont > 0)
        //    {
        //        return View("Error");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Registrar");
        //    }
        // }

        public ActionResult Update(FormCollection fc)
        {
            string[] cantidades = fc.GetValues("Cantidad");
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Cantidad = Convert.ToInt32(cantidades[i]);
            }
            return View("Carrito");
        }
    }
}