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
    
    public class PRODUCTOSController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: PRODUCTOS
        public ActionResult Index()
        {
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");
            ViewBag.ListaProducto = db.PRODUCTOS.ToList();
            return View();
        }

        // GET: PRODUCTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS pRODUCTOS = db.PRODUCTOS.Find(id);
            if (pRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTOS);
        }


        // GET: PRODUCTOS/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");
            return View();
        }

        // POST: PRODUCTOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,CODIGOBARRAS,NOMBRE,PRECIO,DESCRIPCION,SERIE,MARCA,MODELO,CATEGORIASID")] PRODUCTOS pRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                var con = new SqlConnection("Data Source=DESKTOP-95OQ87C\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
                con.Open();
                db.PRODUCTOS.Add(pRODUCTOS);
                db.SaveChanges();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.ALMACENs (CANTIDAD, PRODUCTOSID) VALUES (0, " + pRODUCTOS.ID + ")", con);
                //var command = new SqlCommand();
                command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            else
            {
                if (pRODUCTOS.DESCRIPCION == null)
                {
                    pRODUCTOS.DESCRIPCION = "N/A";
                }
                if (pRODUCTOS.SERIE == null)
                {
                    pRODUCTOS.SERIE = "N/A";
                }
                if (pRODUCTOS.MARCA == null)
                {
                    pRODUCTOS.MARCA = "N/A";
                }
                if (pRODUCTOS.MODELO == null)
                {
                    pRODUCTOS.MODELO = "N/A";
                }
                if (pRODUCTOS.NOMBRE == null|| pRODUCTOS.CODIGOBARRAS == null || pRODUCTOS.PRECIO == 0 || pRODUCTOS.CATEGORIASID == 0)
                {
                    ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE");
                    ViewBag.ListaProducto = db.PRODUCTOS.ToList();
                    return View(pRODUCTOS);
                }
                var con = new SqlConnection("Data Source=DESKTOP-95OQ87C\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
                con.Open();
                db.PRODUCTOS.Add(pRODUCTOS);
                db.SaveChanges();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.ALMACENs (CANTIDAD, PRODUCTOSID) VALUES (0, " + pRODUCTOS.ID + ")", con);
                //var command = new SqlCommand();
                command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
                
        }

        // GET: PRODUCTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS pRODUCTOS = db.PRODUCTOS.Find(id);
            if (pRODUCTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE", pRODUCTOS.CATEGORIASID);
            return View(pRODUCTOS);
        }

        // POST: PRODUCTOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODIGOBARRAS,NOMBRE,PRECIO,UBICACION,CATEGORIASID")] PRODUCTOS pRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORIASID = new SelectList(db.CAT_PRODUCTO, "ID", "NOMBRE", pRODUCTOS.CATEGORIASID);
            return View(pRODUCTOS);
        }

        // GET: PRODUCTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS pRODUCTOS = db.PRODUCTOS.Find(id);
            if (pRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTOS);
        }

        // POST: PRODUCTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTOS pRODUCTOS = db.PRODUCTOS.Find(id);
            db.PRODUCTOS.Remove(pRODUCTOS);
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
