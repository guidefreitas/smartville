using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smartville.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController() { }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult System()
        {
            return View();
        }

        [HttpGet]
        public ActionResult App()
        {
            return View();
        }
    }
}
