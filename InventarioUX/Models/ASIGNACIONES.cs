using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class ASIGNACIONES
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("CONTAINING_EMPLEADOS")]
        public int EMPLEADOID { get; set; }
        public virtual EMPLEADOS CONTAINING_EMPLEADOS { get; set; }

        [ForeignKey("CONTAINING_DEPARTAMENTOS")]
        public int DEPARTAMENTOID { get; set; }
        public virtual DEPARTAMENTOS CONTAINING_DEPARTAMENTOS { get; set; }
    }
}