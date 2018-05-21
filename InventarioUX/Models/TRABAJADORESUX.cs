using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class TRABAJADORESUX
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido Paterno")]
        public string AP_PATERNO { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido Materno")]
        public string AP_MATERNO { get; set; }

        [Display(Name = "Departamento")]
        [ForeignKey("CONTAINING_DEPARTAMENTOS")]
        public int DEPARTAMENTOID { get; set; }

        public virtual DEPARTAMENTOS CONTAINING_DEPARTAMENTOS { get; set; }
    }
}