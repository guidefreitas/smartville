using Smartville.Models;
using Smartville.Areas.Admin.ViewModels;
using Smartville.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Smartville.Filters;
using System.Data.Entity;

namespace Smartville.Areas.Admin.Controllers
{
    [AuthorizationFilter(UserType.GlobalAdministrator)]
    public class SensorController : BaseController
    {

        public SensorController() { }

        public ActionResult Index()
        {
            List<Sensor> sensors = this.db.Sensors
                                          .Include(m => m.City)
                                          .OrderByDescending(c => c.Id).ToList();

            return View(sensors);
        }

        public ActionResult Detail(long id)
        {
            try
            {
                var sensor = this.db.Sensors
                                    .Where(m => m.Id == id)
                                    .Include(m => m.Statuses)
                                    .Include(m => m.City)
                                    .FirstOrDefault();
                if(sensor == null)
                {
                    return HttpNotFound();
                }


                return View(sensor);
            }
            catch
            {
                this.FlashError("Ocorreu um problema ao buscar o sensor");
                return RedirectToAction("Index");
            }
            

        }

        [HttpGet]
        public ActionResult Create()
        {
            Sensor sensor = new Sensor();

            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sensor vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Sensor sensor = new Sensor();
                    sensor.Name = vm.Name;
                    sensor.CreatedAt = DateTime.Now;
                    sensor.UpdatedAt = DateTime.Now;
                    this.db.Sensors.Add(sensor);
                    this.db.SaveChanges();
                    this.FlashInfo("Sensor cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o sensor");
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
