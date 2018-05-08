using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class TIPO_EMPLEADOS
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string TIPO { get; set; }

        public virtual ICollection<EMPLEADOS> EMPLEADOS { get; set; }
    }
}