using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _db;
        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ShopViewDiv(string txtunos)
        {
            List<ShowCarsVM> model = _db.Car.Select(s => new ShowCarsVM
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
                Transmission = s.Transmission.TransmissionType
            }).ToList();

            var shoplista = model.Where(s => txtunos == "" || txtunos == null || (s.Brand.ToLower().Contains(txtunos.ToLower())
                           || s.CarModel.ToLower().Contains(txtunos.ToLower()))).ToList();

            ViewData["txtUnosKey"] = txtunos;
            ViewData["ShopListKey"] = shoplista;


            return View();
        }
        public IActionResult CarDetails(int id)
        {

            var car = _db.Car
                .Where(i => i.ID == id)
                .SingleOrDefault();

            //var car = _db.Car.Find(model.CarId);

            var modelDetails = new ShowCarDetailsVM
            {
                carID = car.ID,
                Brand = _db.Brand.Where(s => s.ID == car.BrandID).Select(s => s.BrandName).FirstOrDefault(),
                Model = car.Model,
                FuelID = car.FuelID,
                Fuel = _db.Fuel.Where(s => s.ID == car.FuelID).Select(s => s.FuelName).FirstOrDefault(),
                VehicleTypeID = car.VehicleTypeID,
                VehicleType = _db.VehicleType.Where(s => s.ID == car.VehicleTypeID).Select(s => s.TypeName).FirstOrDefault(),
                ColorID = car.ColorID,
                Color = _db.Color.Where(s => s.ID == car.ColorID).Select(s => s.ColorName).FirstOrDefault(),
                DriveTypeID = car.DriveTypeID,
                DriveType = _db.DriveType.Where(s => s.ID == car.DriveTypeID).Select(s => s.DriveTypeName).FirstOrDefault(),
                TransmissionID = car.TransmissionID,
                Transmission = _db.Transmission.Where(s => s.ID == car.TransmissionID).Select(s => s.TransmissionType).FirstOrDefault(),
                NumberOfSeats = car.NumberOfSeats,
                NumberOfDors = car.NumberOfDors,
                NumberOfGears = car.NumberOfGears,
                PowerPS = car.PowerPS,
                PowerKw = car.PowerKw,
                WheelSize = car.WheelSize,
                Ccm = car.Ccm,
                Kilometre = car.Kilometre,
                DateOfManufacture = car.DateOfManufacture.ToString("dd/MM.yyyy")
            };

            return View(modelDetails);
        }
        public IActionResult AddToCart(int id)
        {
            var car = _db.Car
                .Where(i => i.ID == id)
                .SingleOrDefault();

            var claimsIdentiti = User.Identity as ClaimsIdentity;


            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;

                    var item = new ShoppingCart
                    {
                        CarId = car.ID,
                        Car = car,
                        UserId = Int32.Parse(userIdValue),

                    };
                    _db.Add(item);
                    _db.SaveChanges();
                }
            }

            return Redirect("/Car/Index");
        }
        public IActionResult ShoppingCart()
        {
            var claimsIdentiti = User.Identity as ClaimsIdentity;

            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    List<ShoppingCartVM> cartModel = _db.ShoppingCart
                        .Where(i => i.UserId == Int32.Parse(userIdValue))
                        .Select(i => new ShoppingCartVM
                        {
                            CartId = i.Id,
                            CarId = i.CarId,
                            BrandId = i.Car.BrandID,
                            Brand = _db.Brand.Where(s => s.ID == i.Car.BrandID).Select(s => s.BrandName).FirstOrDefault(),
                            Model = i.Car.Model,
                            ModelId = 1,
                            Fuel = _db.Fuel.Where(s => s.ID == i.Car.FuelID).Select(s => s.FuelName).FirstOrDefault(),
                            FuelId = 1,
                            NumberOfDors = i.Car.NumberOfDors
                        }).ToList();

                    return View(cartModel);

                }
                //return View();
            }
            return View();
        }
        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCart = _db.ShoppingCart.Where(i => i.Id == id).FirstOrDefault();
            _db.ShoppingCart.Remove(shoppingCart);
            _db.SaveChanges();

            return Redirect("/Shop/ShoppingCart");
        }
    }
}
