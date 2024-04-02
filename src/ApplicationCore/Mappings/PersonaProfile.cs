using ApplicationCore.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappings
{
    public class PersonaProfile: Profile
    {
        public PersonaProfile() 
        {
            CreateMap<CreatePersonaCommand, persona>().ForMember(x => x.PkPersona, y => y.Ignore());
        }
    }
}
