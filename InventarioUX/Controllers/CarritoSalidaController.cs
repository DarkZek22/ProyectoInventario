﻿using System;
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
    public class CarritoSalidaController : Controller
    {
        private ConnectionContext db = new ConnectionContext();

        public ActionResult Index()
        {
            var mOV_SALIDA = db.MOV_SALIDA.Include(m => m.CONTAINING_EMPLEADOS).Include(m => m.CONTAINING_DEPARTAMENTOS);
            return View(mOV_SALIDA.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOV_SALIDA mOV_SALIDA = db.MOV_SALIDA.Find(id);
            if (mOV_SALIDA == null)
            {
                return HttpNotFound();
            }
            List<int> result = new List<int>();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT ID FROM MOV_SALIDA_LISTA WHERE MOV_SALIDAID='" + id + "'", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["ID"]));
            }
            List<MOV_SALIDA_LISTA> listaProductos = new List<MOV_SALIDA_LISTA>();
            int i = 0;
            foreach (var w in result)
            {
                var x = db.MOV_SALIDA_LISTA.Find(result[i]);
                MOV_SALIDA_LISTA x2 = new MOV_SALIDA_LISTA();
                x2.CONTAINING_PRODUCTOS = x.CONTAINING_PRODUCTOS;
                x2.PRECIO = x.PRECIO;
                x2.CANTIDAD = x.CANTIDAD;
                listaProductos.Add(x2);
                i++;
            }
            ViewBag.ListaSalida = listaProductos;
            return View(mOV_SALIDA);
        }

        public ActionResult SelectProducto(FormCollection fc)
        {
            return View();
        }

        public ActionResult codigobarras(FormCollection fc)
        {
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();

            string[] cantidad = fc.GetValues("codigobarras");
            int codigobarras = Convert.ToInt32(cantidad[0]);

            var command = new SqlCommand("SELECT ID FROM dbo.PRODUCTOS WHERE CODIGOBARRAS='" + codigobarras + "'", con);
            int id = (int)(command.ExecuteScalar());

            return RedirectToAction("Agregar", new { id = id });
        }

        public ActionResult Carrito()
        {
            return View();
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count(); i++)
            {
                if (cart[i].Producto.ID == id)
                    return i;

            }
            return -1;
        }

        public ActionResult Eliminar(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Carrito");
        }

        public ActionResult Agregar(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var wea = db.PRODUCTOS.Find(id);
                Item wea2 = new Item();
                wea2.Producto = wea;
                wea2.Cantidad = 1;
                cart.Add(wea2);
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    var wea = db.PRODUCTOS.Find(id);
                    Item wea2 = new Item();
                    wea2.Producto = wea;
                    wea2.Cantidad = 1;
                    cart.Add(wea2);
                }
                else
                {
                    cart[index].Cantidad++;
                }

                Session["cart"] = cart;
            }
            return View("Carrito");
        }

        public ActionResult CheckOut()
        {
            ViewBag.DEPARTAMENTOID = new SelectList(db.DEPARTAMENTOS, "ID", "NOMBRE");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "DEPARTAMENTOID")] MOV_SALIDA mov_salida)
        {
            Session["Departamento"] = mov_salida.DEPARTAMENTOID;
            return View("Carrito");
        }

        public ActionResult Registrar()
        {
            MOV_SALIDA mov_salida = new MOV_SALIDA();
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            Item i = new Item();
            List<Item> cart = (List<Item>)Session["cart"];
            mov_salida.EMPLEADOID = int.Parse(Session["ID"].ToString());
            mov_salida.DEPARTAMENTOID = int.Parse(Session["Departamento"].ToString());
            mov_salida.Fecha = DateTime.Now;
            db.MOV_SALIDA.Add(mov_salida);
            db.SaveChanges();

            foreach (Item item in cart)
            {
                MOV_SALIDA_LISTA mov_salida_lista = new MOV_SALIDA_LISTA();
                mov_salida_lista.PRODUCTOID = item.Producto.ID;
                mov_salida_lista.CANTIDAD = item.Cantidad;
                mov_salida_lista.PRECIO = item.Producto.PRECIO;
                mov_salida_lista.MOV_SALIDAID = mov_salida.ID;
                db.MOV_SALIDA_LISTA.Add(mov_salida_lista);
                db.SaveChanges();

                SqlCommand command = new SqlCommand("UPDATE PRODUCTOS SET CANTIDAD = CANTIDAD - " + item.Cantidad + " WHERE ID = " + item.Producto.ID + "", con);
                command.ExecuteNonQuery();
            }
            Session.Remove("cart");
            Session.Remove("Departamento");
            return View("OrderSaved");
        }

        public ActionResult Cancelar()
        {
            return View("");
        }

        public ActionResult Existencias()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            var con = new SqlConnection("Data Source=DESKTOP-I5C9AA0\\SQLEXPRESS2008;Initial Catalog=InventarioUXBD;Integrated Security=True");
            con.Open();
            int cont = 0;
            foreach (Item item in cart)
            {
                var command2 = new SqlCommand("SELECT CANTIDAD FROM dbo.PRODUCTOS WHERE ID=" + item.Producto.ID + "", con);
                int existencia = (int)(command2.ExecuteScalar());
                if (item.Cantidad>existencia)
                {
                    cont++;
                }
            }
            if (cont > 0)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction("Registrar");
            }
        }

        public ActionResult Update(FormCollection fc)
        {
            string[] cantidades = fc.GetValues("Cantidad");
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Cantidad = Convert.ToInt32(cantidades[i]);
            }
            return View("Carrito");
        }
    }
}