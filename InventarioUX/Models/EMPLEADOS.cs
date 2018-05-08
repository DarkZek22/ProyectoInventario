using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class EMPLEADOS
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Usuario")]
        public string USERNAME { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Contraseña")]
        public string PASSWORD { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Apellido Paterno")]
        public string AP_PATERNO { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Apellido Materno")]
        public string AP_MATERNO { get; set; }

        [Display(Name = "Tipo de empleado")]
        [ForeignKey("CONTAINING_TIPO_EMPLEADO")]
        public int TIPO_EMPLEADOID { get; set; }

        public virtual TIPO_EMPLEADOS CONTAINING_TIPO_EMPLEADO { get; set; }

        public virtual ICollection<ASIGNACIONES> ASIGNACIONES { get; set; }
        public virtual ICollection<MOV_ENTRADA> MOV_ENTRADA { get; set; }
        public virtual ICollection<MOV_SALIDA> MOV_SALIDA { get; set; }
    }
}