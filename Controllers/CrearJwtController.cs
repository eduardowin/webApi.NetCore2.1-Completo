using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiEsFeDemostracion.Entities;
using apiEsFeDemostracion.Helpers;
using apiEsFeDemostracion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace apiEsFeDemostracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearJwtController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CrearJwtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UsuarioTokenDto>> CreateUser([FromBody] EFacUsuario model)
        {
            //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            var oJwtHelper = new JwtHelper(_configuration);
            
            return oJwtHelper.BuildToken(model);
            //}
            //else
            //{
            //    return BadRequest("Username or password invalid");
            //}

        }
    }
}