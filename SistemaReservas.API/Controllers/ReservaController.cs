using Microsoft.AspNetCore.Mvc;

namespace SistemaReservas.API.Controllers
{
    public class ReservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
