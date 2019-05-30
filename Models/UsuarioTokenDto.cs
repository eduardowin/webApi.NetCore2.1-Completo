using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiEsFeDemostracion.Models
{
    public class UsuarioTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
