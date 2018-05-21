using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class DEPARTAMENTOS
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }


        [ForeignKey("CONTAINING_AREAS")]
        public int AREAID { get; set; }
        public virtual AREAS CONTAINING_AREAS { get; set; }

        public virtual ICollection<ASIGNACIONES> ASIGNACIONES { get; set; }
        public virtual ICollection<MOV_SALIDA> MOV_SALIDA { get; set; }
        public virtual ICollection<PUESTOS> PUESTOS { get; set; }
        public virtual ICollection<TRABAJADORESUX> TRABAJADORESUX { get; set; }
    }
}