using DistanciaEntreDosCoordenadas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DistanciaEntreDosCoordenadas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CalcularLasDistancia(double latitudPunto1, double longitudPunto1, double latitudPunto2, double longitudPunto2)
        {
            double distancia = CalcularDistancia(latitudPunto1, longitudPunto1, latitudPunto2, longitudPunto2);
            ViewBag.Distancia = distancia;
            ViewBag.LatitudPunto1 = latitudPunto1;
            ViewBag.LongitudPunto1 = longitudPunto1;
            ViewBag.LatitudPunto2 = latitudPunto2;
            ViewBag.LongitudPunto2 = longitudPunto2;
            return View("Index");
        }
        private  double CalcularDistancia(double latitud1, double longitud1, double latitud2, double longitud2)
        {
            // Convertir a radianes
            double latitudRad1 = Math.PI * latitud1 / 180.0;
            double longitudRad1 = Math.PI * longitud1 / 180.0;
            double latitudRad2 = Math.PI * latitud2 / 180.0;
            double longitudRad2 = Math.PI * longitud2 / 180.0;

            // Radio de la Tierra en kilómetros
            double radioTierra = 6371.0;

            // Calcular las diferencias de latitud y longitud
            double deltaLatitud = latitudRad2 - latitudRad1;
            double deltaLongitud = longitudRad2 - longitudRad1;

            // Calcular la distancia utilizando la fórmula de Haversine
            double a = Math.Sin(deltaLatitud / 2) * Math.Sin(deltaLatitud / 2) +
                       Math.Cos(latitudRad1) * Math.Cos(latitudRad2) * Math.Sin(deltaLongitud / 2) * Math.Sin(deltaLongitud / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distancia = radioTierra * c;

            return distancia;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
