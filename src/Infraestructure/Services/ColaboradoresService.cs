using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ApplicationCore.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Update;
namespace Infraestructure.Services
{
    public class ColaboradoresService : IColaboradoresService
    {
        private readonly ApplicationDbContext _dbcontext;

        public ColaboradoresService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Response<object>> GetColaboradores()
        {
            object list = new object();
            list = await _dbcontext.colaboradores.ToListAsync();
            return new Response<object>(list);
        }


        // modifica el controlador para que me permita hacer el filtro de fecha de todo los colaboradores creados en fechaInicio a FechaEnd, y que si no quiero filtrar por fecha pueda filtrar por edad, y si no quiero ninguna tambien pueda por si es profesor o admin, el cual me da si el Isprofesor es true o false
        public async Task<IEnumerable<ColaboradorDTO>> GetColaboradoresFiltrados(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? edad = null, bool? isProfesor = null)
        {
            var query = _dbcontext.Set<Colaboradores>().AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(c => c.FechaCreacion >= fechaInicio && c.FechaCreacion <= fechaFin);
            }
            else if (edad.HasValue)
            {
                query = query.Where(c => c.Edad == edad);
            }

            if (isProfesor.HasValue)
            {
                query = query.Where(c => c.IsProfesor == isProfesor.Value);
            }

            var colaboradores = await query
                .Select(c => new ColaboradorDTO
                {
                    Nombre = c.Nombre,
                    Edad = c.Edad,
                    BirthDate = c.BirthDate,
                    IsProfesor = c.IsProfesor,
                    FechaCreacion = c.FechaCreacion
                })
                .ToListAsync();

            return colaboradores;
        }

    }
}
