using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class DEPARTAMENTOS
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        public virtual ICollection<ASIGNACIONES> ASIGNACIONES { get; set; }
        public virtual ICollection<MOV_SALIDA> MOV_SALIDA { get; set; }
    }
}