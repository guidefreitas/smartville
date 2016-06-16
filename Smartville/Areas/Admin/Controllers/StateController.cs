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
    public class StateController : BaseController
    {
        public ActionResult Index()
        {
            var states = this.db.States.OrderBy(m => m.Id).ToList();
            return View(states);
        }

        [HttpGet]
        public ActionResult Create()
        {
            VMEditCreateState vm = new VMEditCreateState();
            vm.Countries = db.Countries.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMEditCreateState vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new State();
                    state.Name = vm.Name;
                    state.Code = vm.Code;
                    state.Country = db.Countries.Where(c => c.Id == vm.CountryId).FirstOrDefault();
                    this.db.States.Add(state);
                    this.db.SaveChanges();
                    this.FlashInfo("Estado cadastrado com sucesso.");
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
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o estado");
                }

            }

            vm.Countries = db.Countries.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            State state = this.db.States
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (state == null)
                return HttpNotFound();

            VMEditCreateState vm = new VMEditCreateState();
            vm.Countries = db.Countries.OrderBy(m => m.Name).ToList();
            vm.CountryId = state.Country.Id;
            vm.Name = state.Name;
            vm.Id = state.Id;
            vm.Code = state.Code;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, VMEditCreateState vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = this.db.States
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

                    if (state == null)
                        return HttpNotFound();

                    state.Name = vm.Name;
                    state.Code = vm.Code;
                    state.UpdatedAt = DateTime.Now;
                    this.db.SaveChanges();
                    this.FlashInfo("Estado alterado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um problema ao salvar o estado");
                }

            }
            vm.Countries = db.Countries.OrderBy(m => m.Name).ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            State state = this.db.States
                                  .Where(c => c.Id == id)
                                  .FirstOrDefault();

            if (state == null)
                return HttpNotFound();


            return View(state);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, State vm)
        {
            ModelState.Clear();
            State state = this.db.States
                                  .Where(c => c.Id == vm.Id)
                                  .FirstOrDefault();

            if (state == null)
                return HttpNotFound();

            if (state.Cities.Count > 0)
            {
                this.FlashError("Você não pode apagar esse estado pois ele tem cidades cadastradas. Remova todas as cidades antes de apagar o estado");
                return View(state);
            }

            try
            {
                db.States.Remove(state);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                this.FlashError("Ocorreu um problema ao apagar o estado");
            }

            return View(state);
        }
    }
}