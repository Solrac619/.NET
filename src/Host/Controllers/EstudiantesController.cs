using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using System;
using System.Data;
using PdfSharp.Pdf;
using System.IO;
using System.Linq;
using Domain.Entities;

namespace Host.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {

        private readonly IEstudiantesService _service;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;


        public EstudiantesController(IEstudiantesService service, IMediator mediator, ApplicationDbContext context)
        {
            _service = service;
            _mediator = mediator;
            _context = context;

        }

        [HttpGet("getEstudiantes")]
        public async Task<IActionResult> GetEstudiantes()
        {
            var result = await _service.GetEstudiantes();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Response<int>>> CreateEstudiante(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudentes(int id)
        {
            var Estudiantes = await _context.Estudiantes.FindAsync(id);

            if (Estudiantes == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(Estudiantes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("createEstudiantesPdf")]
        public async Task<IActionResult> CreateEstudiantesPdf()
        {
            XFont fontBold = new XFont("Verdana", 12, XFontStyleEx.Bold);
            XFont fontNormal = new XFont("Verdana", 12, XFontStyleEx.Regular);

            // Llamada al servicio para obtener los estudiantes
            var estudiantesResponse = await _service.GetEstudiantes();

            // Verificar si la respuesta es válida
            if (!estudiantesResponse.Succeeded || estudiantesResponse.Data == null || !estudiantesResponse.Data.Any())
            {
                return NotFound("No se encontraron estudiantes.");
            }

            var estudiantesList = estudiantesResponse.Data; // Obtener la lista de estudiantes

            // Crear el documento PDF
            using (var document = new PdfDocument())
            {
                document.Info.Title = "Lista de Estudiantes";
                var page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                page.Orientation = PdfSharp.PageOrientation.Portrait;

                var gfx = XGraphics.FromPdfPage(page);
                gfx.DrawString("Lista de Estudiantes", fontBold, XBrushes.Black, 20, 40);
                gfx.DrawString("ID", fontNormal, XBrushes.Black, 20, 80);
                gfx.DrawString("Nombre", fontNormal, XBrushes.Black, 100, 80);
                gfx.DrawString("Email", fontNormal, XBrushes.Black, 300, 80);

                int yPosition = 100;
                foreach (var estudiante in estudiantesList)
                {
                    gfx.DrawString(estudiante.id.ToString(), fontNormal, XBrushes.Black, 20, yPosition);
                    gfx.DrawString(estudiante.nombre, fontNormal, XBrushes.Black, 100, yPosition);
                    gfx.DrawString(estudiante.correo, fontNormal, XBrushes.Black, 300, yPosition);
                    yPosition += 20;
                }

                using (var stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    stream.Position = 0;
                    return File(stream.ToArray(), "application/pdf", "Estudiantes.pdf");
                }
            }
        }

    }
}

    
    


