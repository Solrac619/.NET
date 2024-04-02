using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ApplicationCore.Wrappers;
using ApplicationCore.Commands;
using ApplicationCore.DTOs;

namespace Host.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly IMediator _mediator;
        public DashboardController(IDashboardService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        //<summary>
        //Get de todos los elementos
        //</summary>

        [Route("getData")]
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var result = await _service.GetData();
            return Ok(result);
        }

        //<summary>
        //Create de Personas
        //</summary>
        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> Create([FromBody] PersonaDto request)
        {
            var result = await _service.CreatePersona(request);
            //var result = await _mediator.Send(request);
            return Ok(result);
        }

        [Route("getIP")]
        [HttpGet]
        public async Task<IActionResult> GetIpAddress()
        {
            var result = await _service.GetClientIpAddress();
            return Ok(result);
        }

        //<summary>
        //Create de Logs
        //</summary>
        [HttpPost("logs")]
        public async Task<ActionResult<Response<int>>> Create([FromBody] LogDto request)
        {
            var result = await _service.CreateLog(request);
            return Ok(result);
        }

        //JUGADOR 

        //<summary>
        //Create de Personas
        //</summary>
        [HttpPost("create-jugador")]
        public async Task<ActionResult<Response<int>>> CreateJugador([FromBody] jugadorDto request)
        {
            var result = await _service.CreateJugador(request);
            return Ok(result);
        }

        //<summary>
        //update de jugador
        //</summary>
        [HttpPost("update-jugador")]
        public async Task<ActionResult<Response<int>>> UpdateJugador([FromBody] jugadorDto request)
        {
            var result = await _service.UpdateJugador(request);
            return Ok(result);
        }

        //<summary>
        //delete de jugador
        //</summary>
        [HttpPost("delete/{pk}")]
        public async Task<ActionResult<Response<int>>> deleteJugador([FromRoute] int pk)
        {
            var result = await _service.deleteJugador(pk);
            return Ok(result);
        }
    }
}
