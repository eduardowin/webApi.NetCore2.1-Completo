using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiEsFeDemostracion.Entities;

namespace apiEsFeDemostracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesosController : ControllerBase
    {
        private readonly DbContextFe _context;

        public AccesosController(DbContextFe context)
        {
            _context = context;
        }

        // GET: api/Accesos
        [HttpGet]
        public IEnumerable<EFacAcceso> GetEFacAcceso()
        {
            return _context.EFacAcceso;
        }

        // GET: api/Accesos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEFacAcceso([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eFacAcceso = await _context.EFacAcceso.FindAsync(id);

            if (eFacAcceso == null)
            {
                return NotFound();
            }

            return Ok(eFacAcceso);
        }

        // PUT: api/Accesos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEFacAcceso([FromRoute] string id, [FromBody] EFacAcceso eFacAcceso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eFacAcceso.AccesoId)
            {
                return BadRequest();
            }

            _context.Entry(eFacAcceso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EFacAccesoExists(id))
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

        // POST: api/Accesos
        [HttpPost]
        public async Task<IActionResult> PostEFacAcceso([FromBody] EFacAcceso eFacAcceso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EFacAcceso.Add(eFacAcceso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EFacAccesoExists(eFacAcceso.AccesoId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEFacAcceso", new { id = eFacAcceso.AccesoId }, eFacAcceso);
        }

        // DELETE: api/Accesos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEFacAcceso([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eFacAcceso = await _context.EFacAcceso.FindAsync(id);
            if (eFacAcceso == null)
            {
                return NotFound();
            }

            _context.EFacAcceso.Remove(eFacAcceso);
            await _context.SaveChangesAsync();

            return Ok(eFacAcceso);
        }

        private bool EFacAccesoExists(string id)
        {
            return _context.EFacAcceso.Any(e => e.AccesoId == id);
        }
    }
}