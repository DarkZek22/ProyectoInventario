using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class MOV_ENTRADA
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }

        public int PRECIOTOTAL { get; set; }

        [ForeignKey("CONTAINING_EMPLEADOS")]
        public int EMPLEADOID { get; set; }
        public virtual EMPLEADOS CONTAINING_EMPLEADOS { get; set; }

        [ForeignKey("CONTAINING_PROVEEDORES")]
        public int PROVEEDORID { get; set; }
        public virtual PROVEEDORES CONTAINING_PROVEEDORES { get; set; }

    }
}