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

        public async Task<IEnumerable<ColaboradorDTO>> GetColaboradoresByFechaIngreso(DateTime fechaIngreso)
        {
            // Consulta a la base de datos para obtener los colaboradores con fecha de creación posterior a la fecha de ingreso
            var colaboradores = await _dbcontext.Set<Colaboradores>()
                .Where(c => c.FechaCreacion >= fechaIngreso)
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
    