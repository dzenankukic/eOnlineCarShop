using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class CarController : Controller
    {
        private ApplicationDbContext _db;
        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShowCarsVM> model = _db.Car.Select(s => new ShowCarsVM
            {
                Brand = s.Brand,
                CarModel = s.Model,
                NumberOfSeats = s.NumberOfSeats,
                NumberOfDors = s.NumberOfDors,
                NumberOfGears = s.NumberOfGears,
                PowerKw = s.PowerKw,
                PowerPS = s.PowerPS,
                Ccm = s.Ccm,
                WheelSize = s.WheelSize,
                Kilometre = s.Kilometre,
                DateOfManufacture = s.DateOfManufacture,
                Fuel = s.Fuel.FuelName,
                VehicleType = s.VehicleType.TypeName,
                Color = s.Color.ColorName,
                DriveType = s.DriveType.DriveTypeName,
                Transmission = s.Transmission.TransmissionType
            }).ToList();

            return View(model);
        }

        public IActionResult AddCar()
        {
            AddCarVM model = new AddCarVM();

            model.FuelName = _db.Fuel.Select(f => new SelectListItem
            {
                Value = f.ID.ToString(),
                Text = f.FuelName
            }).ToList();

            model.TypeName = _db.VehicleType.Select(vt => new SelectListItem
            {
                Value = vt.ID.ToString(),
                Text = vt.TypeName
            }).ToList();

            model.Color = _db.Color.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.ColorName
            }).ToList();

            model.DriveType = _db.DriveType.Select(dt => new SelectListItem
            {
                Value = dt.ID.ToString(),
                Text = dt.DriveTypeName
            }).ToList();

            model.Transmission = _db.Transmission.Select(t => new SelectListItem
            {
                Value = t.ID.ToString(),
                Text = t.TransmissionType
            }).ToList();

            return View(model);
        }

        public IActionResult Save(AddCarVM model)
        {
            Car newCar = new Car
            {
                Brand = model.Brand,
                Model = model.CarModel,
                Fuel = _db.Fuel.Where(s => s.ID == model.FuelID).SingleOrDefault(),
                VehicleType = _db.VehicleType.Where(s=>s.ID==model.VehicleTypeID).SingleOrDefault(),
                Color = _db.Color.Where(s=> s.ID==model.ColorID).SingleOrDefault(),
                DriveType = _db.DriveType.Where(s=> s.ID==model.DriveTypeID).SingleOrDefault(),
                Transmission = _db.Transmission.Find(model.TransmissionID),
                NumberOfSeats = model.NumberOfSeats,
                NumberOfDors = model.NumberOfDors,
                NumberOfGears = model.NumberOfGears,
                PowerPS = model.PowerPS,
                PowerKw = model.PowerKw,
                WheelSize = model.WheelSize,
                Ccm = model.Ccm,
                Kilometre = model.Kilometre,
                DateOfManufacture = model.DateOfManufacture
            };
            
            _db.Add(newCar);
            _db.SaveChanges();

            return Redirect(url:"/Car/Index");
        }
    }
}
