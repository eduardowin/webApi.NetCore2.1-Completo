using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apiEsFeDemostracion.Entities
{
    public partial class EFacMenu
    {
        public EFacMenu()
        {
            EFacAcceso = new HashSet<EFacAcceso>();
        }

        public int MenuId { get; set; }
        public string NombreMnu { get; set; }
        public string HtmlMnu { get; set; }
        public byte? OrdenMnu { get; set; }
        [Range(1,1000 )]
        public int? PadreMnu { get; set; }
        public bool? EstadoMnu { get; set; }
        public byte? VisibleMnu { get; set; }

        public ICollection<EFacAcceso> EFacAcceso { get; set; }
    }
}
