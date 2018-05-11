using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class AREAS
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Display(Name = "Activo")]
        public bool ACTIVO { get; set; }

        public virtual ICollection<DEPARTAMENTOS> DEPARTAMENTOS { get; set; }
    }
}