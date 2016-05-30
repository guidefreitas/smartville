using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smartville.Controllers
{
    public class MapController : BaseController
    {
        public MapController() { }

        public ActionResult Index()
        {
            return View();
        }
    }
}
