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
        Task<IEnumerable<ColaboradorDTO>> GetColaboradoresByFechaIngreso(DateTime fechaIngreso);

    }
}