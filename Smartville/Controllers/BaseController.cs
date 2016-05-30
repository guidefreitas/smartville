using Smartville.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseContext db = null;

        public BaseController()
        {
            this.db = new DatabaseContext();
        }
    }
}
