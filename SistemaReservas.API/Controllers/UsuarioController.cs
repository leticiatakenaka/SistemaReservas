using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Application.Interfaces;

namespace SistemaReservas.API.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService) => this.usuarioAppService = usuarioAppService;

        [HttpGet("")]
        public async Task<IActionResult> Usuarios(
            [FromQuery] int page = 1,      
            [FromQuery] int pageSize = 10, 
            [FromQuery] string termoPesquisa = "",
            [FromQuery] bool? ativo = null) 
        {
            var resultado = await usuarioAppService.ObterUsuariosAsync(page, pageSize, termoPesquisa, ativo);

            return Ok(new
            {
                success = true,
                data = resultado.Items,
                total = resultado.TotalCount,
                page = resultado.Page,
                pageSize = resultado.PageSize
            });
        }
    }
}
