using Smartville.Models;
using Smartville.Areas.Admin.ViewModels;
using Smartville.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Smartville.Filters;

namespace Smartville.Areas.Admin.Controllers
{
    [AuthorizationFilter(UserType.GlobalAdministrator)]
    public class CountryController : BaseController
    {

        public CountryController() { }

        public ActionResult Index()
        {
            List<Country> countries = this.db.Countries.OrderBy(c => c.Name).ToList();

            return View(countries);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Country country = new Country();

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country vm)
        {
            if (ModelState.IsValid)
            {
                Country country = new Country();
                this.db.Countries.Add(country);
                this.db.SaveChanges();
                RedirectToAction("Index");
            }

            return View(vm);
        }


    }
}
