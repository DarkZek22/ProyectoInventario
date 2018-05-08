using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class ASIGNACIONES_LISTA
    {
        public int ID { get; set; }
        public int PRECIO { get; set; }


        [ForeignKey("CONTAINING_PRODUCTOS")]
        public int PRODUCTOID { get; set; }
        public virtual PRODUCTOS CONTAINING_PRODUCTOS { get; set; }

        [ForeignKey("CONTAINING_ASIGNACION")]
        public int ASIGNACIONID { get; set; }
        public virtual ASIGNACIONES CONTAINING_ASIGNACION { get; set; }
    }
}