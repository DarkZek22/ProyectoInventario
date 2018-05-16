using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class PROVEEDORES
    {
        [Key]
        public int ID { get; set; }

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

        [Required]
        [StringLength(60)]
        [Display(Name = "Direccion")]
        public string DIRECCION { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Codigo de barras invalido")]
        [Display(Name = "Telefono")]
        public string TELEFONO { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Correo Electronico")]
        public string EMAIL { get; set; }

        public virtual ICollection<MOV_ENTRADA> MOV_ENTRADA { get; set; }
    }
}