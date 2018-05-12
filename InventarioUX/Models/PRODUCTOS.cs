using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class PRODUCTOS
    {
        public PRODUCTOS()
        {
            UBICACION = "Almacen"; //set default value here
            CANTIDAD = 0;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Codigo de Barras")]
        [StringLength(15, MinimumLength = 12, ErrorMessage = "Codigo de barras invalido")]
        public string CODIGOBARRAS { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public int PRECIO { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int CANTIDAD { get; set; }

        [StringLength(120)]
        public string UBICACION { get; set; }



        [Display(Name = "Categoria de Producto")]
        [ForeignKey("CONTAINING_CATEGORIAS")]
        public int CATEGORIASID { get; set; }

        public virtual CAT_PRODUCTOS CONTAINING_CATEGORIAS { get; set; }

        public virtual ICollection<ASIGNACIONES_LISTA> ASIGNACIONES_LISTA { get; set; }
        public virtual ICollection<MOV_ENTRADA_LISTA> MOV_ENTRADA_LISTA { get; set; }
        public virtual ICollection<MOV_SALIDA_LISTA> MOV_SALIDA_LISTA { get; set; }
    }
}