using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class MOV_ENTRADA_LISTA
    {
        public int ID { get; set; }
        public int CANTIDAD { get; set; }
        public int PRECIO { get; set; }


        [ForeignKey("CONTAINING_PRODUCTOS")]
        public int PRODUCTOID { get; set; }
        public virtual PRODUCTOS CONTAINING_PRODUCTOS { get; set; }

        [ForeignKey("CONTAINING_MOV_ENTRADA")]
        public int MOV_ENTRADAID { get; set; }
        public virtual MOV_ENTRADA CONTAINING_MOV_ENTRADA { get; set; }
    }
}