using ApplicationCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEstudiantesService
    {
        Task<Response<object>> GetEstudiantes();

    }
}
