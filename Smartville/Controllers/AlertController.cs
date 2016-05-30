using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smartville.Controllers
{
    public class AlertController : BaseController
    {
        public AlertController() { }

        public ActionResult Index()
        {
            List<Comunicate> alerts = new List<Comunicate>();
            for(int i = 0; i < 10; i++)
            {
                Comunicate alert = new Comunicate();
                alert.Title = "Titulo do alerta " + i;
                alert.Message = "Alerta de exemplo " + i;
                alert.Link = "http://google.com";
                alert.DatePublicated = DateTime.Now;
                alerts.Add(alert);
            }

            return View(alerts);
        }
    }
}
