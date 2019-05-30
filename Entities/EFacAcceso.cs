using System;
using System.Collections.Generic;

namespace apiEsFeDemostracion.Entities
{
    public partial class EFacAcceso
    {
        public string AccesoId { get; set; }
        public string RucCia { get; set; }
        public string RolId { get; set; }
        public int MenuId { get; set; }
        public string UsuarioId { get; set; }

        public EFacMenu Menu { get; set; }
        public EFacRol Rol { get; set; }
    }
}
