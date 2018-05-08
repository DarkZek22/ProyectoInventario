using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventarioUX.Models;

namespace InventarioUX.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            Session.Remove("cart");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(EMPLEADOS empleado)
        {
            //if (ModelState.IsValid)
            //{
            using (ConnectionContext db = new ConnectionContext())
            {
                var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
                con.Open();
                var usr = db.EMPLEADOS.Where(u => u.USERNAME == empleado.USERNAME && u.PASSWORD == empleado.PASSWORD).FirstOrDefault();
                if (usr != null)
                {
                    var x = empleado.USERNAME;
                    var command = new SqlCommand("SELECT ID FROM dbo.EMPLEADOS WHERE USERNAME='" + x + "'", con);
                    int result = (int)(command.ExecuteScalar());
                    Session["ID"] = result.ToString();
                    Session["USERNAME"] = empleado.USERNAME.ToString();
                    return RedirectToAction("Logeado");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o Contraseña invalido");
                }
            }
            //}
            return View();
        }

        public ActionResult Logeado()
        {
            if (Session["ID"] != null)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["USERNAME"] = null;
            return RedirectToAction("Login");
        }
    }
}