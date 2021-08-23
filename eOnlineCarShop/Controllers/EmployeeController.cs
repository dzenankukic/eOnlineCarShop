using Data_CS.Data;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers() // metoda koja vraca podatke za Angular
        {
            var model = _db.User
                .Select(s => new ShowUsersVM {
                userid = s.Id,
                firstname = s.FirstName,
                lastname = s.LastName,
                birthdate = s.BirthDate.ToString(),
                username = s.UserName,
                email = s.Email,
                emailconfirmed = s.EmailConfirmed,
                phonenumber = s.PhoneNumber,
                phonenumberconfirmed = s.PhoneNumberConfirmed
            }).ToList();

            return Ok(model);
        }

<<<<<<< HEAD
       /* public IActionResult ShowFinishedItems()
=======
        public IActionResult ShowFinishedItems()
>>>>>>> a54ead3fe9a5f3919d7f1e6b9364398426046677
        {
            List<ShowCarsVM> model = _db.FinishedItems
                .Select(s => new ShowCarsVM
                {
                    CarId = s.ID,
                    Brand = s.brand.BrandName,
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
                    Transmission = s.Transmission.TransmissionType,
                    DateOfFinish = s.DateOfFinish
                }).ToList();
<<<<<<< HEAD
        
            return View(model);
        }*/
=======

            return View(model);
        }
>>>>>>> a54ead3fe9a5f3919d7f1e6b9364398426046677
    }
}
