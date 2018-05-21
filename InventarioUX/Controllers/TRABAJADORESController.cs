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
    public class TRABAJADORESController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: TRABAJADORES
        public ActionResult Index()
        {
            var tRABAJADORESUX = db.TRABAJADORESUX.Include(t => t.CONTAINING_DEPARTAMENTOS);
            return View(tRABAJADORESUX.ToList());
        }

        // GET: TRABAJADORES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRABAJADORESUX tRABAJADORESUX = db.TRABAJADORESUX.Find(id);
            if (tRABAJADORESUX == null)
            {
                return HttpNotFound();
            }
            return View(tRABAJADORESUX);
        }

        // GET: TRABAJADORES/Create
        public ActionResult Create()
        {
            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE");
            return View();
        }

        // POST: TRABAJADORES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,AP_PATERNO,AP_MATERNO,DEPARTAMENTOID")] TRABAJADORESUX tRABAJADORESUX)
        {
            if (ModelState.IsValid)
            {
                db.TRABAJADORESUX.Add(tRABAJADORESUX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE", tRABAJADORESUX.DEPARTAMENTOID);
            return View(tRABAJADORESUX);
        }

        // GET: TRABAJADORES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRABAJADORESUX tRABAJADORESUX = db.TRABAJADORESUX.Find(id);
            if (tRABAJADORESUX == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE", tRABAJADORESUX.DEPARTAMENTOID);
            return View(tRABAJADORESUX);
        }

        // POST: TRABAJADORES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,AP_PATERNO,AP_MATERNO,DEPARTAMENTOID")] TRABAJADORESUX tRABAJADORESUX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRABAJADORESUX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE", tRABAJADORESUX.DEPARTAMENTOID);
            return View(tRABAJADORESUX);
        }

        // GET: TRABAJADORES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRABAJADORESUX tRABAJADORESUX = db.TRABAJADORESUX.Find(id);
            if (tRABAJADORESUX == null)
            {
                return HttpNotFound();
            }
            return View(tRABAJADORESUX);
        }

        // POST: TRABAJADORES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRABAJADORESUX tRABAJADORESUX = db.TRABAJADORESUX.Find(id);
            db.TRABAJADORESUX.Remove(tRABAJADORESUX);
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
