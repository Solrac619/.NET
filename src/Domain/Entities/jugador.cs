using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("jugador")]
    public class jugador
    {
        [Key]
        public int pkJugador { get; set; }
        public string Nombre { get; set; }
        public string posicion { get; set; }
    }
}
