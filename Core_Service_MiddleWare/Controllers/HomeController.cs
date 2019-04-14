using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core_Service_MiddleWare.Controllers
{
   
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello From HomeController");
        }

        public IActionResult Index2()
        {
            return Content("Index2");
        }
    }
}