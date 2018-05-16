using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventarioUX.Models;

namespace InventarioUX.Controllers
{
    public class PROVEEDORESController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: PROVEEDORES
        public ActionResult Index()
        {
            ViewBag.ListaProveedores = db.PROVEEDORES.ToList();
            return View();
        }

        // GET: PROVEEDORES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES pROVEEDORES = db.PROVEEDORES.Find(id);
            if (pROVEEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDORES);
        }

        // GET: PROVEEDORES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PROVEEDORES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,NOMBRE,AP_PATERNO,AP_MATERNO,DIRECCION,TELEFONO,EMAIL")] PROVEEDORES pROVEEDORES)
        {
            if (ModelState.IsValid)
            {
                db.PROVEEDORES.Add(pROVEEDORES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ListaProveedores = db.PROVEEDORES.ToList();
                return View(pROVEEDORES);
            }           
        }

        // GET: PROVEEDORES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES pROVEEDORES = db.PROVEEDORES.Find(id);
            if (pROVEEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDORES);
        }

        // POST: PROVEEDORES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,AP_PATERNO,AP_MATERNO,DIRECCION,TELEFONO,EMAIL")] PROVEEDORES pROVEEDORES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROVEEDORES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROVEEDORES);
        }

        // GET: PROVEEDORES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES pROVEEDORES = db.PROVEEDORES.Find(id);
            if (pROVEEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDORES);
        }

        // POST: PROVEEDORES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROVEEDORES pROVEEDORES = db.PROVEEDORES.Find(id);
            db.PROVEEDORES.Remove(pROVEEDORES);
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
