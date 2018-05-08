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
    public class CAT_PRODUCTOSController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: CAT_PRODUCTOS
        public ActionResult Index()
        {
            return View(db.CAT_PRODUCTO.ToList());
        }

        // GET: CAT_PRODUCTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAT_PRODUCTOS cAT_PRODUCTOS = db.CAT_PRODUCTO.Find(id);
            if (cAT_PRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(cAT_PRODUCTOS);
        }

        // GET: CAT_PRODUCTOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CAT_PRODUCTOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE")] CAT_PRODUCTOS cAT_PRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                db.CAT_PRODUCTO.Add(cAT_PRODUCTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAT_PRODUCTOS);
        }

        // GET: CAT_PRODUCTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAT_PRODUCTOS cAT_PRODUCTOS = db.CAT_PRODUCTO.Find(id);
            if (cAT_PRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(cAT_PRODUCTOS);
        }

        // POST: CAT_PRODUCTOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE")] CAT_PRODUCTOS cAT_PRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAT_PRODUCTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAT_PRODUCTOS);
        }

        // GET: CAT_PRODUCTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAT_PRODUCTOS cAT_PRODUCTOS = db.CAT_PRODUCTO.Find(id);
            if (cAT_PRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(cAT_PRODUCTOS);
        }

        // POST: CAT_PRODUCTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAT_PRODUCTOS cAT_PRODUCTOS = db.CAT_PRODUCTO.Find(id);
            db.CAT_PRODUCTO.Remove(cAT_PRODUCTOS);
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
