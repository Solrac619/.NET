using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IColaboradoresService
    {
        Task<Response<object>> GetColaboradores();
        Task<IEnumerable<ColaboradorDTO>> GetColaboradoresFiltrados(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? edad = null, bool? isProfesor = null);


    }
}