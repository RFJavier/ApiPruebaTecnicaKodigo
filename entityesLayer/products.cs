using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityesLayer
{
    public class products
    {
        [Key]
        public int idproduct { get; set; }

        [ForeignKey("idcategory")]
        [Display(Name = "products")]
        public int idcategory { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de producto")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string productname { get; set; }

        [Required(ErrorMessage = "Ingrese el codigo de producto")]
        public string code { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad del inventario")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Ingrese la descripcion del producto")]
        [StringLength(400, ErrorMessage = "Maximo 400 caracteres")]
        public string descriptions { get; set; }

        public int statusproduct { get; set; }

        [NotMapped]
        public productCategory ProductCategories { get; set; }
    }
    public enum Estatus_Product
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
