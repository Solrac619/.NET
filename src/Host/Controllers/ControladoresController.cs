using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using Infraestructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Data;
using System;

namespace Host.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradoresService _service;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        

        public ColaboradoresController(IColaboradoresService service, IMediator mediator, ApplicationDbContext context)
        {
            _service = service;
            _mediator = mediator;
            _context = context;
        }
        [HttpGet("getColaboradoresByFechaIngreso")]
        public async Task<IActionResult> GetColaboradoresByFechaIngreso([FromQuery] DateTime fechaIngreso)
        {
            // Llamada al servicio para obtener los colaboradores
            var colaboradores = await _service.GetColaboradoresByFechaIngreso(fechaIngreso);
            // Si no se encontraron colaboradores, retornamos un 404
            if (colaboradores == null || !colaboradores.Any())
            {
                return NotFound("No se encontraron colaboradores."); // Retorna un 404 con un mensaje
            }
            // Si se encuentran colaboradores, retornamos los datos
            return Ok(colaboradores); // Retorna los datos en formato OK
        }

        [HttpPost("create")]
        public async Task<Response<int>> CreateColaborador(ColaboradorDTO colaboradoresDto)
        {
            var colaborador = new Colaboradores
            {
                Nombre = colaboradoresDto.Nombre,
                Edad = colaboradoresDto.Edad,
                BirthDate = colaboradoresDto.BirthDate,
                IsProfesor = colaboradoresDto.IsProfesor,
                FechaCreacion = colaboradoresDto.FechaCreacion,
            };

            _context.colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();


            if (colaborador.IsProfesor)
            {
                var profesor = new Domain.Entities.Profesor
                {
                    FkColaborador = colaborador.Id
                };
                _context.profesors.Add(profesor);
            }
            else
            {
                var admin = new Domain.Entities.Administrativo
                {
                    FkColaborador = colaborador.Id
                };
                _context.administrativos.Add(admin);
            }

            await _context.SaveChangesAsync();

            return new Response<int>(colaborador.Id);
        }
    }
}

 