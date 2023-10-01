using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;


namespace entityesLayer
{
    public class rol
    {
        [Key]
        public int idrol { get; set; }

        [Required (ErrorMessage = "ingrese un rol para usuarios")] 
        public string rolname { get; set; }
       
        [NotMapped]
        public int Top_Aux { get; set; }

        [NotMapped]
        public List<registeredUsers> registeredusers{ get; set; }
    }

}
