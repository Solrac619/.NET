using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Persona")]
    public class persona
    {
        [Key]
        public int PkPersona { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string ColorFav { get; set; }
        public string CancionFav { get; set; }
        public string ComidaFav { get; set; }
    }
}
