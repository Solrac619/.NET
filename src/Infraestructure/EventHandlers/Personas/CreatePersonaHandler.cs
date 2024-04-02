using ApplicationCore.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApplicationCore.Commands;
using Infraestructure.Persistence;
using ApplicationCore.Interfaces;
using ApplicationCore.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Infraestructure.EventHandlers.Personas
{
    public class CreatePersonaHandler : IRequestHandler<CreatePersonaCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;

        public CreatePersonaHandler(ApplicationDbContext context, IMapper mapper, IDashboardService dashboardService)
        {
            _context = context;
            _mapper = mapper;
            _dashboardService = dashboardService;
        }

        //public class CreatePersonaCommandValidator : AbstractValidator<CreatePersonaCommand>
        //{
        //    public CreatePersonaCommandValidator()
        //    {
        //        RuleFor(x => x.ComidaFav).NotEmpty().WithMessage("El campo ComidaFav no puede estar vacío.");
        //        // Agrega más reglas de validación según tus necesidades
        //    }
        //}


        public async Task<Response<int>> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var p = new CreatePersonaCommand();
                p.Nombre = request.Nombre;
                p.Ciudad = request.Ciudad;
                p.ComidaFav = request.ComidaFav;
                p.ColorFav = request.ColorFav;
                p.CancionFav = request.CancionFav;

                var pe = _mapper.Map<Domain.Entities.persona>(p);
                await _context.persona.AddAsync(pe);
                var req = await _context.SaveChangesAsync(cancellationToken);
                var res = new Response<int>(pe.PkPersona, "Registro creado");

                //logs
                var log = new LogDto();
                log.Datos = "Datos";
                log.fecha = DateTime.Now.ToString();
                log.NomFuncion = "Create";
                log.mensaje = res.Message;
                log.StatusLog = "200";

                await _dashboardService.CreateLog(log);

                return res;
            }
            catch (DbUpdateException ex)
            {
                // Manejar la excepción específica de Entity Framework Core
                var errorLog = new LogDto();
                errorLog.Datos = "Datos";
                errorLog.fecha = DateTime.Now.ToString();
                errorLog.NomFuncion = "Create";
                errorLog.mensaje = $"Error al crear el registro: {ex.Message}";
                errorLog.StatusLog = "500";

                await _dashboardService.CreateLog(errorLog);

                // Puedes lanzar la excepción nuevamente o devolver una respuesta de error
                throw new Exception("Error al guardar los cambios en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                var errorLog = new LogDto();
                errorLog.Datos = "Datos";
                errorLog.fecha = DateTime.Now.ToString();
                errorLog.NomFuncion = "Create";
                errorLog.mensaje = $"Error desconocido al crear el registro: {ex.Message}";
                errorLog.StatusLog = "500";

                await _dashboardService.CreateLog(errorLog);

                // Puedes lanzar la excepción nuevamente o devolver una respuesta de error
                throw;
            }

        }

        //public async Task<Response<int>> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreatePersonaCommandValidator();
        //    var validationResult = await validator.ValidateAsync(request);

        //    if (!validationResult.IsValid)
        //    {
        //        var validationErrorLog = new LogDto
        //        {
        //            Datos = "Datos",
        //            fecha = DateTime.Now.ToString(),
        //            NomFuncion = "Create",
        //            mensaje = $"Error de validación del modelo: {string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))}",
        //            StatusLog = "400"
        //        };

        //        await _dashboardService.CreateLog(validationErrorLog);

        //        return new Response<int>(0, "Error de validación del modelo");
        //    }

        //    // Resto del código...
        //    var p = new CreatePersonaCommand();
        //    p.Nombre = request.Nombre;
        //    p.Ciudad = request.Ciudad;
        //    p.ComidaFav = request.ComidaFav;
        //    p.ColorFav = request.ColorFav;
        //    p.CancionFav = request.CancionFav;

        //    var pe = _mapper.Map<Domain.Entities.persona>(p);
        //    await _context.persona.AddAsync(pe);
        //    var req = await _context.SaveChangesAsync();
        //    var res = new Response<int>(pe.PkPersona, "Registro creado");

        //    //logs
        //    var log = new LogDto();
        //    log.Datos = "Datos";
        //    log.fecha = DateTime.Now.ToString();
        //    log.NomFuncion = "Create";
        //    log.mensaje = res.Message;
        //    log.StatusLog = "200";

        //    await _dashboardService.CreateLog(log);
        //    return res;
        //}
    }
}
