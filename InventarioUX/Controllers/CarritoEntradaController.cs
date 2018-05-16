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
using Rotativa;

namespace InventarioUX.Controllers
{
    public class CarritoEntradaController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        /////////
        ///////// Lista de movimientos
        /////////
        public ActionResult Index()
        {
            var mOV_ENTRADA = db.MOV_ENTRADA.Include(m => m.CONTAINING_EMPLEADOS).Include(m => m.CONTAINING_PROVEEDORES);
            return View(mOV_ENTRADA.ToList());
        }

        /////////
        ///////// Detalles de un movimiento
        /////////
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOV_ENTRADA mOV_ENTRADA = db.MOV_ENTRADA.Find(id);
            if (mOV_ENTRADA == null)
            {
                return HttpNotFound();
            }
            List<int> result = new List<int>();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT ID FROM MOV_ENTRADA_LISTA WHERE MOV_ENTRADAID='" + id + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<MOV_ENTRADA_LISTA> listaProductos = new List<MOV_ENTRADA_LISTA>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.MOV_ENTRADA_LISTA.Find(result[i]);
                MOV_ENTRADA_LISTA x2 = new MOV_ENTRADA_LISTA();
                x2.CONTAINING_PRODUCTOS = x.CONTAINING_PRODUCTOS;
                x2.PRECIO = x.PRECIO;
                x2.CANTIDAD = x.CANTIDAD;
                listaProductos.Add(x2);
                i++;
            }
            ViewBag.ListaEntrada = listaProductos;
            return View(mOV_ENTRADA);
        }

        /////////
        ///////// Seleccion de productos
        /////////
        public ActionResult SelectProducto(FormCollection fc)
        {
            return View();
        }

        /////////
        ///////// Recibe un codigo de barras y lo busca, redirecciona a "Agregar" el id correspondiente a ese codigo de barras
        /////////
        public ActionResult codigobarras(FormCollection fc)
        {
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();

            string[] cantidad = fc.GetValues("codigobarras");
            //int codigobarras = Convert.ToInt32(cantidad[0]);

            var command = new SqlCommand("SELECT ID FROM dbo.PRODUCTOS WHERE CODIGOBARRAS='"+ cantidad[0] + "'", con);
            int id = (int)(command.ExecuteScalar());

            return RedirectToAction("Agregar", new { id = id});
        }

        /////////
        /////////
        /////////
        public ActionResult Carrito()
        {
            return View();
        }

        /////////
        /////////
        /////////
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

        /////////
        ///////// Elimina un producto
        /////////
        public ActionResult Eliminar(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Carrito");
        }

        /////////
        ///////// Agrega el producto seleccionado en "codigobarras" al carrito
        /////////
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


        /////////
        ///////// Selecciona un proveedor
        /////////
        public ActionResult CheckOut()
        {
            ViewBag.PROVEEDORID = new SelectList(db.PROVEEDORES, "ID", "NOMBRE");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "PROVEEDORID")] MOV_ENTRADA mov_entrada)
        {
            Session["Proveedor"] = mov_entrada.PROVEEDORID;
            return View("Carrito"); 
        }

        /////////
        /////////Registro del movimiento y cada producto en el
        /////////
        public ActionResult Registro()
        {
            MOV_ENTRADA mov_entrada = new MOV_ENTRADA();
            List<Item> cart = (List<Item>)Session["cart"];
            int preciototal = 0;
            mov_entrada.EMPLEADOID = int.Parse(Session["ID"].ToString());
            mov_entrada.PROVEEDORID = int.Parse(Session["Proveedor"].ToString()); ;
            mov_entrada.Fecha = DateTime.Now;
            db.MOV_ENTRADA.Add(mov_entrada);
            db.SaveChanges();

            foreach (Item item in cart)
            {
                var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
                con.Open();
                MOV_ENTRADA_LISTA mov_entrada_lista = new MOV_ENTRADA_LISTA();
                mov_entrada_lista.PRODUCTOID = item.Producto.ID;
                mov_entrada_lista.CANTIDAD = item.Cantidad;
                mov_entrada_lista.PRECIO = item.Producto.PRECIO;
                preciototal = preciototal + (item.Producto.PRECIO * item.Cantidad);
                mov_entrada_lista.MOV_ENTRADAID = mov_entrada.ID;
                db.MOV_ENTRADA_LISTA.Add(mov_entrada_lista);
                db.SaveChanges();

                SqlCommand command = new SqlCommand("UPDATE PRODUCTOS SET CANTIDAD = CANTIDAD + " + item.Cantidad + " WHERE ID = " + item.Producto.ID + "", con);
                command.ExecuteNonQuery();
            }
            mov_entrada.PRECIOTOTAL = preciototal;
            db.SaveChanges();
            Session.Remove("Proveedor");
            Session.Remove("cart");
            preciototal = 0;
            return View("OrderSaved");
        }

        public ActionResult Cancelar()
        {
            return View("");
        }

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

        public ActionResult ReciboEntrada (int? id){
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOV_ENTRADA mOV_ENTRADA = db.MOV_ENTRADA.Find(id);
            if (mOV_ENTRADA == null)
            {
                return HttpNotFound();
            }
            List<int> result = new List<int>();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT ID FROM MOV_ENTRADA_LISTA WHERE MOV_ENTRADAID='" + id + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<MOV_ENTRADA_LISTA> listaProductos = new List<MOV_ENTRADA_LISTA>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.MOV_ENTRADA_LISTA.Find(result[i]);
                MOV_ENTRADA_LISTA x2 = new MOV_ENTRADA_LISTA();
                x2.CONTAINING_PRODUCTOS = x.CONTAINING_PRODUCTOS;
                x2.PRECIO = x.PRECIO;
                x2.CANTIDAD = x.CANTIDAD;
                listaProductos.Add(x2);
                i++;
            }
            ViewBag.ListaEntrada = listaProductos;
            return View(mOV_ENTRADA);
        }

        public ActionResult ExportPDF(int? id)
        {
            return new ActionAsPdf("ReciboEntrada", new { id = id })
            {
                FileName = Server.MapPath("~/Content/Relato.pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4
            };
        }
    }

}
