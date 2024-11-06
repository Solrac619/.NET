using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ColaboradorDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime();
        public bool IsProfesor { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;


    }

}
