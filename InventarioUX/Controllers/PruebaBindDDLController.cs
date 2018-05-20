using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventarioUX.Models;

namespace InventarioUX.Controllers
{
    public class PruebaBindDDLController : Controller
    {
        private ConnectionContext db = new ConnectionContext();
        // GET: PruebaBindDDL
        public ActionResult Index()
        {
            ViewBag.DEPARTAMENTOS = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE");
            return View();
        }

        public JsonResult GetStateById(int DeptoID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PUESTOS> PuestoList = db.PUESTOS.Where(x => x.DEPARTAMENTOID == DeptoID).ToList();
            return Json(PuestoList, JsonRequestBehavior.AllowGet);
        }
    }
}