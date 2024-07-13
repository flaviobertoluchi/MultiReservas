using Microsoft.AspNetCore.Mvc;
using MultiReservas.Extensions;
using MultiReservas.Models;
using System.Diagnostics;

namespace MultiReservas.Controllers
{
    [UsuarioAutorizacao]
    public class HomeController : Controller
    {
        public IActionResult Index()
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
