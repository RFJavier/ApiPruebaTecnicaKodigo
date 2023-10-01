using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entityesLayer;

namespace entityesLayer
{
    public class productCategory
    {
        [Key]
        public int idcategory { get; set; }

        [Required(ErrorMessage = "Ingrese nombre de categoria")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string categoryname { get; set; }
        
        public int statuscategory { get; set; }

        [NotMapped]
        public List<products> Products { get; set; }
    }
    public enum Estatus_category
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
