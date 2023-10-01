using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace entityesLayer
{
    public class registeredUsers
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("idrol")]
        [Display(Name = "rol")]
        public int idrol { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string username { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre de usuario")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        public string nickname { get; set; }

        [Required(ErrorMessage = "Ingrese su correo")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        [StringLength(32, ErrorMessage = "Maximo 32 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "userpassword")]
        public string userpassword { get; set; }

        public int isactive { get; set; }

        [Display(Name = "registerdate")]
        public DateTime registerdate { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
        [NotMapped]
        public rol rol { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar el password")]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("userpassword", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar password")]
        public string ConfirmPassword_aux { get; set; }
    }
    public enum Estatus_Usuario
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
