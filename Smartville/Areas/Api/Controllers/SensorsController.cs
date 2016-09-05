using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smartville.Models;
using Smartville.Areas.Api.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;
using Smartville.Controllers;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Smartville.Areas.Api.Controllers
{
    public class SensorsController : BaseController
    {
        public SensorsController()
        {
            if(this.db.Sensors.Count() == 0)
            {
                City city;
                State state;
                Country country;
                if(db.Countries.Count() == 0)
                {
                    country = new Country { Name = "Brasil" };
                    db.Countries.Add(country);
                    db.SaveChanges();
                }
                else
                {
                    country = db.Countries.Where(c => c.Name == "Brasil").FirstOrDefault();
                }

                if(db.States.Count() == 0)
                {
                    state = new State { Name = "Santa Catarina", Country = country, Code = "SC" };
                    db.States.Add(state);
                    db.SaveChanges();
                }else
                {
                    state = db.States.Where(s => s.Name == "Santa Catarina").FirstOrDefault();
                }

                if(db.Cities.Count() == 0)
                {
                    city = new City { Name = "Joinville", State = state };
                    db.Cities.Add(city);
                    db.SaveChanges();
                }else
                {
                    city = db.Cities.Where(c => c.Name == "Joinville").FirstOrDefault();
                }

                for(int i = 0; i < 100; i++)
                {
                    Sensor sensor = new Sensor();
                    sensor.Address = "Rua XV de Novembro " + i;
                    sensor.City = city;
                    sensor.Latitude = 10.0;
                    sensor.Longitude = 10.0;
                    sensor.Name = "Sensor " + i;
                    sensor.SerialNumber = i.ToString();
                    db.Sensors.Add(sensor);
                }

                db.SaveChanges();
                
            }
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var sensors = db.Sensors.OrderBy(m => m.Id).Include(m => m.City).ToList();
            List<SensorViewModel> vm = new List<SensorViewModel>();
            foreach(var s in sensors)
            {
                SensorViewModel sensorVM = new SensorViewModel();
                sensorVM.Address = s.Address;
                if(s.City != null)
                    sensorVM.City = new CityViewModel { Id = s.City.Id, Name = s.City.Name };
                sensorVM.Id = s.Id;
                sensorVM.Latitude = s.Latitude;
                sensorVM.Longitude = s.Longitude;
                vm.Add(sensorVM);
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var sensor = db.Sensors.Where(s => s.Id == id).FirstOrDefault();
            if (sensor == null)
                return Json(@"{ ""error"": ""Sensor not found"" }");

            SensorViewModel vm = new SensorViewModel();
            vm.Id = sensor.Id;
            vm.Address = sensor.Address;
            if(sensor.City != null)
                vm.City = new CityViewModel { Id = sensor.City.Id, Name = sensor.City.Name };
            vm.Latitude = sensor.Latitude;
            vm.Longitude = sensor.Longitude;
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateStatus(string id)
        {
            try
            {
                var sensor = db.Sensors.Where(m => m.SerialNumber == id).FirstOrDefault();
                if(sensor == null)
                    return Content("ERROR|Sensor not found");

                if (Request.Params.AllKeys.Contains("Boia1"))
                {
                    var paramValue = Request.Params.Get("Boia1");
                    var status = new SensorStatus();
                    status.Value = paramValue;
                    status.StatusType = SensorStatusType.FLOOD;
                    status.CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    status.UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    sensor.Statuses.Add(status);
                }

                if (Request.Params.AllKeys.Contains("Boia2"))
                {
                    var paramValue = Request.Params.Get("Boia2");
                    var status = new SensorStatus();
                    status.Value = paramValue;
                    status.StatusType = SensorStatusType.FLOOD;
                    status.CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    status.UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    sensor.Statuses.Add(status);
                }

                if (Request.Params.AllKeys.Contains("Boia3"))
                {
                    var paramValue = Request.Params.Get("Boia3");
                    var status = new SensorStatus();
                    status.Value = paramValue;
                    status.StatusType = SensorStatusType.FLOOD;
                    status.CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    status.UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    sensor.Statuses.Add(status);
                }

                if (Request.Params.AllKeys.Contains("Temperatura"))
                {
                    var paramValue = Request.Params.Get("Temperatura");
                    var status = new SensorStatus();
                    status.Value = paramValue;
                    status.StatusType = SensorStatusType.TEMPERATURE;
                    status.CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    status.UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(sensor.TimeZone));
                    sensor.Statuses.Add(status);
                }

                db.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("ERROR|Internal server error");
            }
            
            
        }

        [HttpGet]
        public ActionResult Statuses(int id)
        {
            try
            {
                var sensor = db.Sensors.Where(s => s.Id == id)
                                       .Include(s => s.Statuses)
                                       .FirstOrDefault();
                if (sensor == null)
                    return Json(@"{ ""error"": ""Sensor not found"" }");

                int countStatus = sensor.Statuses.Count;

                IEnumerable<SensorStatus> statuses = sensor.Statuses
                                     .OrderByDescending(s => s.Id);

                if(Request.Params["take"] != null)
                {
                    int takeParam = Convert.ToInt32(Request.Params["Take"]);
                    if(takeParam > 200)
                    {
                        statuses = statuses.Take(200);
                    }else
                    {
                        statuses = statuses.Take(takeParam);
                    }
                }else
                {
                    statuses = statuses.Take(10);
                }

                if(Request.Params["last"] != null)
                {
                    var lastId = Convert.ToInt64(Request.Params["last"]);
                    statuses = statuses.Where(s => s.Id > lastId);
                }

                List<SensorStatusViewModel> vm = new List<SensorStatusViewModel>();
                foreach (var sensorStatus in statuses)
                {
                    SensorStatusViewModel statusVM = new SensorStatusViewModel();
                    statusVM.StatusId = sensorStatus.Id;
                    statusVM.Value = sensorStatus.Value;
                    statusVM.SensorSerialNumber = sensorStatus.Sensor.SerialNumber;
                    statusVM.StatusType = (int) sensorStatus.StatusType;
                    vm.Add(statusVM);
                }
                
                
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(@"{ ""error"" : ""Internal server error"" }", JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
