using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class LogDto
    {
        //public int idLog { get; set; }
        public string fecha { get; set; }
        public string mensaje { get; set; }
        public string ipAddress { get; set; }
        public string NomFuncion { get; set; }
        public string StatusLog { get; set; }
        public string Datos { get; set; }
    }
}
