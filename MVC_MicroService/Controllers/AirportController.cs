using Microsoft.AspNetCore.Mvc;
using MVC_MicroService.Services;

namespace MVC_MicroService.Controllers
{
    public class AirportController : Controller
    {

        private readonly FrontEndService _service;

        public AirportController(FrontEndService context)
        {
            _service = context;
        }


        public IActionResult Index()
        {
            return View(_service.Get());
        }
    }
}
