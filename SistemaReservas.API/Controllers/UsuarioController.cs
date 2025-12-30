using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Exceptions;
using SistemaReservas.Domain.Interfaces;

namespace SistemaReservas.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioAppService usuarioAppService, IUsuarioRepository usuarioRepository)
        {
            _usuarioAppService = usuarioAppService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterUsuarios(
            [FromQuery] int page = 1,      
            [FromQuery] int pageSize = 10, 
            [FromQuery] string termoPesquisa = "",
            [FromQuery] bool? ativo = null) 
        {
            var resultado = await _usuarioAppService.ObterUsuarios(page, pageSize, termoPesquisa, ativo);

            return Ok(new
            {
                success = true,
                data = resultado.Items,
                total = resultado.TotalCount,
                page = resultado.Page,
                pageSize = resultado.PageSize
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = await _usuarioAppService.ObterUsuarioPorId(id);
                return Ok(usuario);
            }
            catch (EntidadeNaoEncontradaException ex)
            {
                return NotFound(new { success = false, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id, [FromBody] EditarUsuarioDto model)
        {
            try
            {
                var usuario = await _usuarioAppService.EditarUsuario(id, model.Email, model.PrimeiroNome, model.UltimoNome);
                return Ok(usuario);
            }
            catch (EntidadeNaoEncontradaException ex)
            {
                return NotFound(new { success = false, error = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AlterarStatus(Guid id, [FromBody] AlterarStatusDto model)
        {
            try
            {
                var usuario = await _usuarioAppService.AlterarStatus(id, model.Ativo);
                return Ok(usuario);
            }
            catch (EntidadeNaoEncontradaException ex)
            {
                return NotFound(new { success = false, error = ex.Message });
            }
        }
    }
}
