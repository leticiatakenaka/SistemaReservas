using Microsoft.AspNetCore.Mvc;

namespace SistemaReservas.API.Configurations
{
    public class SwaggerConfiguration : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
