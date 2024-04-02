using ApplicationCore.Commands;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace ApplicationCore.Mappings
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogDto, logs>().ForMember(x => x.idLog, y => y.Ignore());
        }
    }
}
