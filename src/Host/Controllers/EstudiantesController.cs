using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Host.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {

        private readonly IEstudiantesService _service;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;


        public EstudiantesController(IEstudiantesService service, IMediator mediator, ApplicationDbContext context)
        {
            _service = service;
            _mediator = mediator;
            _context = context;

        }

        [HttpGet("getEstudiantes")]
        public async Task<IActionResult> GetEstudiantes()
        {
            var result = await _service.GetEstudiantes();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Response<int>>> CreateEstudiante(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudentes(int id)
        {
            var Estudiantes = await _context.Estudiantes.FindAsync(id);

            if (Estudiantes == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(Estudiantes);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    

}
}
