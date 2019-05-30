using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apiEsFeDemostracion.Helpers;

namespace apiEsFeDemostracion.Entities
{
    public partial class EFacRol
    {
        public EFacRol()
        {
            EFacAcceso = new HashSet<EFacAcceso>();
        }

        public string RolId { get; set; }
        public string RucCia { get; set; }
        [Required(ErrorMessage = "El nombre del Rol es obligatorio")]
        [StringLength(maximumLength:50,ErrorMessage = "El campo debe contener {1} caracteres o menos")]
        [PrimeraLetraMayuscula]
        public string NombreRol { get; set; }
        public byte? ActivoRol { get; set; }
            
        public ICollection<EFacAcceso> EFacAcceso { get; set; }
    }
}
