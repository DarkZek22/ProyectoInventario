using InventarioUX.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Rotativa;

namespace InventarioUX.Controllers
{
    public class CarritoAsignacionController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        public ActionResult Index()
        {
            var aSIGNACIONES = db.ASIGNACIONES.Include(a => a.CONTAINING_DEPARTAMENTOS).Include(a => a.CONTAINING_EMPLEADOS);
            return View(aSIGNACIONES.ToList());

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASIGNACIONES mOV_ASIGNACION = db.ASIGNACIONES.Find(id);
            if (mOV_ASIGNACION == null)
            {
                return HttpNotFound();
            }
            List<int> result = new List<int>();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT ID FROM ASIGNACIONES_LISTA WHERE ASIGNACIONID='" + id + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<ASIGNACIONES_LISTA> listaProductos = new List<ASIGNACIONES_LISTA>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.ASIGNACIONES_LISTA.Find(result[i]);
                ASIGNACIONES_LISTA x2 = new ASIGNACIONES_LISTA();
                x2.CONTAINING_PRODUCTOS = x.CONTAINING_PRODUCTOS;
                x2.PRECIO = x.PRECIO;
                x2.CANTIDAD = x.CANTIDAD;
                listaProductos.Add(x2);
                i++;
            }
            ViewBag.ListaAsignacion = listaProductos;
            return View(mOV_ASIGNACION);
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
            string codigobarras = cantidad[0];

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
        public ActionResult Checkout(FormCollection fc)
        {
            string[] DeptoID = fc.GetValues("DeptoID");
            string[] TrabajadorID = fc.GetValues("TrabajadorID");

            string Depto = DeptoID[0].ToString();

            int DID = Int32.Parse(DeptoID[0]);
            int TID = Int32.Parse(TrabajadorID[0]);

            Session["Departamento"] = DID;
            Session["Trabajador"] = TID;
            return View("Carrito");
        }

        public ActionResult Registrar()
        {
            ASIGNACIONES asignaciones = new ASIGNACIONES();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            Item i = new Item();
            List<Item> cart = (List<Item>)Session["cart"];
            int preciototal = 0;
            asignaciones.EMPLEADOID = int.Parse(Session["ID"].ToString());
            int departamentoid = int.Parse(Session["Departamento"].ToString());
            int trabajadorid = int.Parse(Session["Trabajador"].ToString());
            asignaciones.DEPARTAMENTOID = departamentoid;
            asignaciones.Fecha = DateTime.Now;
            db.ASIGNACIONES.Add(asignaciones);
            db.SaveChanges();

            foreach (Item item in cart)
            {
                ASIGNACIONES_LISTA asignaciones_lista = new ASIGNACIONES_LISTA();
                asignaciones_lista.PRODUCTOID = item.Producto.ID;
                asignaciones_lista.PRECIO = item.Producto.PRECIO;
                preciototal = preciototal + (item.Producto.PRECIO * item.Cantidad);
                asignaciones_lista.ASIGNACIONID = asignaciones.ID;
                db.ASIGNACIONES_LISTA.Add(asignaciones_lista);
                db.SaveChanges();
                //Asigna nombre de depto al producto
                SqlCommand command2 = new SqlCommand("SELECT NOMBRE FROM DEPARTAMENTOS WHERE ID = " + departamentoid + "", con);
                string departamentonombre = (string)(command2.ExecuteScalar());
                SqlCommand command = new SqlCommand("UPDATE PRODUCTOS SET UBICACION = '" + departamentonombre + "' WHERE ID = " + item.Producto.ID + "", con);
                command.ExecuteNonQuery();
                //Asigna nombre de empleado al producto
                SqlCommand command3 = new SqlCommand("SELECT NOMBRE FROM TRABAJADORESUXes WHERE ID = " + trabajadorid + "", con);
                string trabajadornombre = (string)(command3.ExecuteScalar());
                SqlCommand command4 = new SqlCommand("UPDATE PRODUCTOS SET EMPLEADOASIGNACION = '" + trabajadornombre + "' WHERE ID = " + item.Producto.ID + "", con);
                command4.ExecuteNonQuery();
            }
            asignaciones.PRECIOTOTAL = preciototal;
            db.SaveChanges();
            Session.Remove("cart");
            Session.Remove("Departamento");
            Session.Remove("Trabajador");
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

        public ActionResult ReciboAsignacion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASIGNACIONES mOV_ASIGNACION = db.ASIGNACIONES.Find(id);
            if (mOV_ASIGNACION == null)
            {
                return HttpNotFound();
            }
            List<int> result = new List<int>();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT ID FROM ASIGNACIONES_LISTA WHERE ASIGNACIONID='" + id + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<ASIGNACIONES_LISTA> listaProductos = new List<ASIGNACIONES_LISTA>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.ASIGNACIONES_LISTA.Find(result[i]);
                ASIGNACIONES_LISTA x2 = new ASIGNACIONES_LISTA();
                x2.CONTAINING_PRODUCTOS = x.CONTAINING_PRODUCTOS;
                x2.PRECIO = x.PRECIO;
                x2.CANTIDAD = x.CANTIDAD;
                listaProductos.Add(x2);
                i++;
            }
            ViewBag.ListaAsignacion = listaProductos;
            return View(mOV_ASIGNACION);
        }

        public ActionResult ExportPDF(int? id)
        {
            return new ActionAsPdf("ReciboAsignacion", new { id = id })
            {
                FileName = Server.MapPath("~/Content/Relato.pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4
            };
        }

        public JsonResult GetStateById(int DeptoID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TRABAJADORESUX> PuestoList = db.TRABAJADORESUX.Where(x => x.DEPARTAMENTOID == DeptoID).ToList();
            return Json(PuestoList, JsonRequestBehavior.AllowGet);
        }
    }
}