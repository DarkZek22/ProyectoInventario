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

        // GET: ALMACENs
        public ActionResult Index()
        {
            var aLMACEN = db.ALMACEN.Include(a => a.CONTAINING_PRODUCTOS);
            return View(aLMACEN.ToList());
        }

        public ActionResult SelectAlmacen()
        {
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectAlmacen([Bind(Include = "CATEGORIASID")] PRODUCTOS productos)
        {
            List<int> result = new List<int>();
            int wea = productos.CATEGORIASID;
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
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
                x2.UBICACION = x.UBICACION;
                listaProductos.Add(x2);
                i++;
            }
            Session["Almacen"] = listaProductos;
            //return View();
            return RedirectToAction("ListaAlmacen");
        }

        public ActionResult ListaAlmacen()
        {
            List<PRODUCTOS> listaProductos = (List<PRODUCTOS>)Session["Almacen"];
            ViewBag.ListaProductos = listaProductos;
            return View();
        }



        // GET: ALMACENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALMACEN aLMACEN = db.ALMACEN.Find(id);
            if (aLMACEN == null)
            {
                return HttpNotFound();
            }
            return View(aLMACEN);
        }

        // GET: ALMACENs/Create
        public ActionResult Create()
        {
            ViewBag.PRODUCTOSID = new SelectList(db.PRODUCTOS, "ID", "NOMBRE");
            return View();
        }

        // POST: ALMACENs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CANTIDAD,PRODUCTOSID")] ALMACEN aLMACEN)
        {
            if (ModelState.IsValid)
            {
                db.ALMACEN.Add(aLMACEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PRODUCTOSID = new SelectList(db.PRODUCTOS, "ID", "NOMBRE", aLMACEN.PRODUCTOSID);
            return View(aLMACEN);
        }

        // GET: ALMACENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALMACEN aLMACEN = db.ALMACEN.Find(id);
            if (aLMACEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCTOSID = new SelectList(db.PRODUCTOS, "ID", "NOMBRE", aLMACEN.PRODUCTOSID);
            return View(aLMACEN);
        }

        // POST: ALMACENs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CANTIDAD,PRODUCTOSID")] ALMACEN aLMACEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aLMACEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PRODUCTOSID = new SelectList(db.PRODUCTOS, "ID", "NOMBRE", aLMACEN.PRODUCTOSID);
            return View(aLMACEN);
        }

        // GET: ALMACENs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALMACEN aLMACEN = db.ALMACEN.Find(id);
            if (aLMACEN == null)
            {
                return HttpNotFound();
            }
            return View(aLMACEN);
        }

        // POST: ALMACENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ALMACEN aLMACEN = db.ALMACEN.Find(id);
            db.ALMACEN.Remove(aLMACEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
