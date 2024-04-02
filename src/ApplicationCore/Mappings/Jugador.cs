using ApplicationCore.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappings
{
    public class Jugador : Profile
    {
        public Jugador()
        {
            CreateMap<jugadorDto, jugador>().ForMember(x => x.pkJugador, y => y.Ignore());
        }
    }
}
