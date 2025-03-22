using Microsoft.AspNetCore.Mvc;

namespace SistemaReservas.API.Middleware
{
    public class ExceptionMiddleware : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
