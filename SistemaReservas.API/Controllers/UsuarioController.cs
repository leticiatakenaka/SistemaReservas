using Microsoft.AspNetCore.Mvc;

namespace SistemaReservas.API.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
