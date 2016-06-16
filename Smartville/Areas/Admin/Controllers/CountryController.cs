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
                try
                {
                    Country country = new Country();
                    country.Name = vm.Name;
                    country.CreatedAt = DateTime.Now;
                    country.UpdatedAt = DateTime.Now;
                    this.db.Countries.Add(country);
                    this.db.SaveChanges();
                    this.FlashInfo("País cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o país");
                }
                
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            Country country = this.db.Countries
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (country == null)
                return HttpNotFound();



            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, Country vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Country country = this.db.Countries
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

                    if (country == null)
                        return HttpNotFound();

                    country.Name = vm.Name;
                    country.UpdatedAt = DateTime.Now;
                    this.db.SaveChanges();
                    this.FlashInfo("País alterado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o país");
                }

            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            Country country = this.db.Countries
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (country == null)
                return HttpNotFound();


            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, Country vm)
        {
            ModelState.Clear();
            Country country = this.db.Countries
                                  .Where(c => c.Id == vm.Id)
                                  .FirstOrDefault();

            if (country == null)
                return HttpNotFound();

            if(country.States.Count > 0)
            {
                this.FlashError("Você não pode apagar esse país pois ele tem estados cadastrados. Remova todos os estados antes de apagar o país");
                return View(vm);
            }

            try
            {
                db.Countries.Remove(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                this.FlashError("Ocorreu um problema ao apagar o país");
            }

            return View(vm);
        }

    }
}
