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
namespace Infraestructure.Services
{
    public class EstudiantesServices : IEstudiantesService
    {
        private readonly ApplicationDbContext _dbcontext;

        public EstudiantesServices(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Response<object>> GetEstudiantes()
        {
            object list = new object();
            list = await _dbcontext.Estudiantes.ToListAsync();
            return new Response<object>(list);  
        }
    }
}
