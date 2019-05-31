using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiEsFeDemostracion.Entities;
using apiEsFeDemostracion.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;

namespace apiEsFeDemostracion.Controllers
{
    /// <summary>
    /// Controlador que permite hacer las llamadas Rest del recurso de roles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolController : ControllerBase
    {
        private readonly DbContextFe _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public RolController(IConfiguration configuration,DbContextFe context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var vInformacion = _configuration["vInformacion"];
        }
        /// <summary>
        /// Metodo Get donde obtiene todos los roles de la tabla
        /// </summary>
        /// <returns>Listado de Roles</returns>
        // GET: api/Rol
        [HttpGet]
        public IEnumerable<EFacRol> GetEFacRol()
        {
            return _context.EFacRol;
        }
        /// Metodo Get donde obtiene todos los roles de la tabla paginado
        /// </summary>
        /// <returns>Listado de Roles</returns>
        // GET: api/Rol
        [HttpGet("GetListaRoles/{numeroDePagina}/{cantidadDeRegistros}")]
        public async Task<ActionResult<IEnumerable<EFacRol>>> GetEFacRol(int numeroDePagina = 1, int cantidadDeRegistros = 10)
        {
            var query = _context.EFacRol.AsQueryable();
            var totalDeRegistros = query.Count();

            var roles = await query
                .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                .Take(cantidadDeRegistros)
                .ToListAsync();

            Response.Headers["X-Total-Registros"] = totalDeRegistros.ToString();
            Response.Headers["X-Cantidad-Paginas"] =((int)Math.Ceiling((double)totalDeRegistros / cantidadDeRegistros)).ToString();

            return roles;
        }
        
        /// <summary>
        /// Metodo Get donde obtiene un rol por su Id
        /// </summary>
        /// <returns>obtjeto de Rol</returns>
        // GET: api/Rol/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEFacRol([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eFacRol = await _context.EFacRol.FindAsync(id);
            if (eFacRol == null)
            {
                return NotFound();
            }
            var oRolDto = _mapper.Map<EfacRolDto>(eFacRol);

            return Ok(oRolDto);
        }
        /// <summary>
        /// Metodo Put para borrar Rol
        /// </summary>
        /// <returns>Retorna no Found cuando esta bien</returns>
        // PUT: api/Rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEFacRol([FromRoute] string id, [FromBody] EFacRol eFacRol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eFacRol.RolId)
            {
                return BadRequest();
            }

            _context.Entry(eFacRol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EFacRolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Metodo Post permite insertar los datos un rol nuevo
        /// </summary>
        /// <returns>Codigo 201</returns>
        // POST: api/Rol
        [HttpPost]
        public async Task<IActionResult> PostEFacRol([FromBody] EFacRol eFacRol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EFacRol.Add(eFacRol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EFacRolExists(eFacRol.RolId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEFacRol", new { id = eFacRol.RolId }, eFacRol);
        }

        // DELETE: api/Rol/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEFacRol([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eFacRol = await _context.EFacRol.FindAsync(id);
            if (eFacRol == null)
            {
                return NotFound();
            }

            _context.EFacRol.Remove(eFacRol);
            await _context.SaveChangesAsync();

            return Ok(eFacRol);
        }

        private bool EFacRolExists(string id)
        {
            return _context.EFacRol.Any(e => e.RolId == id);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(string id, [FromBody] JsonPatchDocument<EfacRolDelDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var rolDeLaDb = await _context.EFacRol.FirstOrDefaultAsync(x => x.RolId == id);

            if (rolDeLaDb == null)
            {
                return NotFound();
            }

            var rolDto = _mapper.Map<EfacRolDelDto>(rolDeLaDb);

            patchDocument.ApplyTo(rolDto, ModelState);

            var isValid = TryValidateModel(rolDeLaDb);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(rolDto, rolDeLaDb);

            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}