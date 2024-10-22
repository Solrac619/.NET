using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Estudiantes")]
    public class Estudiantes
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public int edad {  get; set; }
        public string correo { get; set; }
    }
    
}
