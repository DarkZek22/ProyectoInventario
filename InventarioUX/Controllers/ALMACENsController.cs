using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventarioUX.Models;

namespace InventarioUX.Controllers
{
    public class ALMACENsController : Controller
    {

        private ConnectionContext db = new ConnectionContext();

        public ActionResult SelectAlmacen()
        {
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectAlmacen([Bind(Include = "CATEGORIASID")] PRODUCTOS productos)
        {
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");
            List<int> result = new List<int>();
            int wea = productos.CATEGORIASID;
            var con = new SqlConnection("Data Source=DESKTOP-95OQ87C\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");

            var command = new SqlCommand("SELECT ID FROM dbo.PRODUCTOS WHERE CATEGORIASID='" + wea + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<PRODUCTOS> listaProductos = new List<PRODUCTOS>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.PRODUCTOS.Find(result[i]);
                PRODUCTOS x2 = new PRODUCTOS();
                x2.NOMBRE = x.NOMBRE;
                x2.PRECIO = x.PRECIO;
                x2.CATEGORIASID = x.CATEGORIASID;
                x2.CANTIDAD = x.CANTIDAD;
                x2.UBICACION = x.UBICACION;
                listaProductos.Add(x2);
                i++;
            }
            //Session["Almacen"] = listaProductos;
            ViewBag.ListaProductos = listaProductos;
            return View();
            ///return RedirectToAction("ListaAlmacen");
        }



        public ActionResult ListaAlmacen()
        {
            List<PRODUCTOS> listaProductos = (List<PRODUCTOS>)Session["Almacen"];
            ViewBag.ListaProductos = listaProductos;
            return View("SelectAlmacen");
        }
    }
}
