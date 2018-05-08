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
    public class DEPARTAMENTOSController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        // GET: DEPARTAMENTOS
        public ActionResult Index()
        {
            return View(db.DEPARTAMENTOS.ToList());
        }

        // GET: DEPARTAMENTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DEPARTAMENTOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTAMENTOS.Add(dEPARTAMENTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTOS);
        }

        // POST: DEPARTAMENTOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEPARTAMENTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTOS);
        }

        // POST: DEPARTAMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            db.DEPARTAMENTOS.Remove(dEPARTAMENTOS);
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
