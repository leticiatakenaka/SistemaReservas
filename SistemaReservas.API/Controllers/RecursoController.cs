using Microsoft.AspNetCore.Mvc;

namespace SistemaReservas.API.Controllers
{
    public class RecursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
