using Smartville.Filters;
using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smartville.Areas.Admin.Controllers
{
    [AuthorizationFilter(UserType.GlobalAdministrator)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
