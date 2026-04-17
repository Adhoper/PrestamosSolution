using Microsoft.AspNetCore.Mvc;
using Prestamos.Web.Interfaces;
using Prestamos.Web.Models;

namespace Prestamos.Web.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly IPrestamoApiService _prestamoApiService;

        public PrestamoController(IPrestamoApiService prestamoApiService)
        {
            _prestamoApiService = prestamoApiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PrestamoViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PrestamoViewModel model)
        {
            model.OpcionesMeses = new List<int> { 3, 6, 9, 12 };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _prestamoApiService.CalcularPrestamoAsync(model);

            return View(resultado);
        }
    }
}
