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
    public class TIPO_EMPLEADOSController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: TIPO_EMPLEADOS
        public ActionResult Index()
        {
            return View(db.TIPO_EMPLEADO.ToList());
        }

        // GET: TIPO_EMPLEADOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EMPLEADOS tIPO_EMPLEADOS = db.TIPO_EMPLEADO.Find(id);
            if (tIPO_EMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EMPLEADOS);
        }

        // GET: TIPO_EMPLEADOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPO_EMPLEADOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TIPO")] TIPO_EMPLEADOS tIPO_EMPLEADOS)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_EMPLEADO.Add(tIPO_EMPLEADOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPO_EMPLEADOS);
        }

        // GET: TIPO_EMPLEADOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EMPLEADOS tIPO_EMPLEADOS = db.TIPO_EMPLEADO.Find(id);
            if (tIPO_EMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EMPLEADOS);
        }

        // POST: TIPO_EMPLEADOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TIPO")] TIPO_EMPLEADOS tIPO_EMPLEADOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_EMPLEADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPO_EMPLEADOS);
        }

        // GET: TIPO_EMPLEADOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EMPLEADOS tIPO_EMPLEADOS = db.TIPO_EMPLEADO.Find(id);
            if (tIPO_EMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EMPLEADOS);
        }

        // POST: TIPO_EMPLEADOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPO_EMPLEADOS tIPO_EMPLEADOS = db.TIPO_EMPLEADO.Find(id);
            db.TIPO_EMPLEADO.Remove(tIPO_EMPLEADOS);
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
