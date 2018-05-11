using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class PUESTOS
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string JEFE { get; set; }

        [Display(Name = "Activo")]
        public bool ACTIVO { get; set; }

        [ForeignKey("CONTAINING_DEPARTAMENTOS")]
        public int DEPARTAMENTOID { get; set; }
        public virtual DEPARTAMENTOS CONTAINING_DEPARTAMENTOS { get; set; }
    }
}