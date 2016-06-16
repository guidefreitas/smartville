using Smartville.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Smartville.Areas.Admin.ViewModels;
using Smartville.Models;
using System.Data.Entity.Validation;

namespace Smartville.Areas.Admin.Controllers
{
    public class CityController : BaseController
    {
        public ActionResult Index()
        {
            var cities = this.db.Cities.OrderBy(m => m.Id).ToList();
            return View(cities);
        }

        [HttpGet]
        public ActionResult Create()
        {
            VMEditCreateCity vm = new VMEditCreateCity();
            vm.States = db.States.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMEditCreateCity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    City city = new City();
                    city.Name = vm.Name;
                    city.State = db.States.Where(c => c.Id == vm.StateId).FirstOrDefault();
                    this.db.Cities.Add(city);
                    this.db.SaveChanges();
                    this.FlashInfo("Cidade cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach(var entityError  in ex.EntityValidationErrors)
                    {
                        foreach(var error in entityError.ValidationErrors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar a cidade");
                }

            }

            vm.States = db.States.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            City city = this.db.Cities
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (city == null)
                return HttpNotFound();

            VMEditCreateCity vm = new VMEditCreateCity();
            vm.States = db.States.OrderBy(m => m.Name).ToList();
            vm.StateId = city.State.Id;
            vm.Name = city.Name;
            vm.Id = city.Id;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, VMEditCreateCity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    City city = this.db.Cities
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

                    if (city == null)
                        return HttpNotFound();

                    city.Name = vm.Name;
                    city.UpdatedAt = DateTime.Now;
                    this.db.SaveChanges();
                    this.FlashInfo("Cidade alterada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o estado");
                }

            }
            vm.States = db.States.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            City city = this.db.Cities
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (city == null)
                return HttpNotFound();


            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, City vm)
        {
            ModelState.Clear();
            City city = this.db.Cities
                                  .Where(c => c.Id == vm.Id)
                                  .FirstOrDefault();

            if (city == null)
                return HttpNotFound();

            if (city.Institutes.Count > 0)
            {
                this.FlashError("Você não pode apagar essa cidade pois ela tem institutos cadastrados. Remova todos os insitutos antes de apagar a cidade");
                return View(city);
            }

            try
            {
                db.Cities.Remove(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                this.FlashError("Ocorreu um problema ao apagar a cidade");
            }

            return View(city);
        }
    }
}