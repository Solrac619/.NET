using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Profesor")]
    public class Profesor
    {

        [Key]
        public int Id { get; set; }

        public int FkColaborador { get; set; }

        public string Correo { get; set; }

        public string Departamento { get; set; }

    }
}
