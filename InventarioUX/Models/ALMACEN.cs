using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class ALMACEN
    {
        public int ID { get; set; }
        public int CANTIDAD { get; set; }

        [Display(Name = "Producto")]
        [ForeignKey("CONTAINING_PRODUCTOS")]
        public int PRODUCTOSID { get; set; }

        public virtual PRODUCTOS CONTAINING_PRODUCTOS { get; set; }

    }
}