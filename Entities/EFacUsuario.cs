using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apiEsFeDemostracion.Entities
{
    public partial class EFacUsuario
    {
        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo Ruc es obligatorio")]
        public string RucCia { get; set; }
        public string UsuarioWindows { get; set; }
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        [StringLength(80,ErrorMessage = "El campo usuario debe tener un maximo de 80 caracteres")]
        
        public string NombreUsu { get; set; }
        [EmailAddress(ErrorMessage = "Debe ingresar un email correcto")]
        public string CorreoElectronicoUsu { get; set; }
        public string Contrasenia { get; set; }
        public byte? ActivoUsu { get; set; }
        public bool? AdministradorUsu { get; set; }
    }
}
