using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands
{
    public class CreateLogsCommand : LogDto, IRequest<Response<int>>
    {

    }
}
