using InventarioUX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioUX.Controllers
{
    public class ProductosEntradaController : Controller
    {
        private ConnectionContext db = new ConnectionContext();
        // GET: ListaProductos
        public ActionResult Index()
        {
            ViewBag.listaproductosentrada = db.PRODUCTOS.ToList();
            return View();
        }

    }
}