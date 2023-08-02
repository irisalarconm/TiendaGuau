using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TiendaGuau.Models;
using Microsoft.Data.SqlClient;

namespace TiendaGuau.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TiendaGuauContext _context; 

        public HomeController(ILogger<HomeController> logger, TiendaGuauContext db)
        {
            _logger = logger;
            _context = db;
        }

        [HttpGet]
        [Route("created")]
        public IActionResult CreateDatabase()
        {
            _context.Database.EnsureCreated();
            return Ok();
        }
        public IActionResult Index()
        {
            return View();
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