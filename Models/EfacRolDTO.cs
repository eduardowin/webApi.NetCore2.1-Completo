using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using apiEsFeDemostracion.Helpers;

namespace apiEsFeDemostracion.Models
{
    public class EfacRolDto
    {
        public string RolId { get; set; }
        public string RucCia { get; set; }
        [Required(ErrorMessage = "El nombre del Rol es obligatorio")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo debe contener {1} caracteres o menos")]
        [PrimeraLetraMayuscula]
        public string NombreRol { get; set; }
        public byte? ActivoRol { get; set; }
    }
}
