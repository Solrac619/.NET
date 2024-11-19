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
using Domain.Entities;
using ApplicationCore.Mappings;
namespace Infraestructure.Services
{
    public class EstudiantesServices : IEstudiantesService
    {
        private readonly ApplicationDbContext _dbcontext;

        public EstudiantesServices(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Response<List<Estudiantes>>> GetEstudiantes()
        {
            var estudiantes = await _dbcontext.Estudiantes.ToListAsync();

            // Diagnóstico
            Console.WriteLine("Estudiantes obtenidos:");
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine($"ID: {estudiante.id}, Nombre: {estudiante.nombre}, Correo: {estudiante.correo}");
            }

            if (estudiantes == null || estudiantes.Count == 0)
            {
                return new Response<List<Estudiantes>>(new List<Estudiantes>(), "No se encontraron estudiantes.", false);
            }

            return new Response<List<Estudiantes>>(estudiantes, "Estudiantes obtenidos exitosamente.");
        }

    }
}

