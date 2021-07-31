using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

            foreach (var item in model)
            {
                var carImageSET = _db.CarImage.Where(x => x.CarID == item.CarId).ToList();
                var AllImages = new List<string>();

                foreach (var slika in carImageSET)
                {
                    var ImageEntity = _db.Image.Where(i => i.ID == slika.ImageID).Select(i => i.PathToImage).FirstOrDefault();
                    AllImages.Add(ImageEntity);
                }
                item.images = AllImages;
            }

            return View(model);
        }

        [Authorize]
        public IActionResult AddCar()
        {
            AddCarVM model = new AddCarVM();

            model.Brendovi = _db.Brand.Select(b => new SelectListItem
            {
                Value = b.ID.ToString(),
                Text = b.BrandName
            }).ToList();

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
            Brand tempBrand = _db.Brand.Find(model.BrandID);
            Car newCar = new Car
            {
                brand = tempBrand,
                BrandID = model.BrandID,
                Model = _db.CarModel.Where(s => s.ID == model.CarModelID && s.BrandID == model.BrandID).Select(x => x.NazivModela).SingleOrDefault(),
                Fuel = _db.Fuel.Where(s => s.ID == model.FuelID).SingleOrDefault(),
                VehicleType = _db.VehicleType.Where(s => s.ID == model.VehicleTypeID).SingleOrDefault(),
                Color = _db.Color.Where(s => s.ID == model.ColorID).SingleOrDefault(),
                DriveType = _db.DriveType.Where(s => s.ID == model.DriveTypeID).SingleOrDefault(),
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
            UcitajSlike(model, newCar.ID);
            return Redirect(url: "/Car/Index");
        }

        [HttpPost]
        public IActionResult CarModelGetJson(int BrandID, int? CarModelID)
        {

            var x = _db.CarModel.Where(s => s.BrandID == BrandID).Select(x => new
            {
                ID = x.ID,
                NazivModela = x.NazivModela
            }).ToList();

            if (CarModelID != null)
            {
                x = _db.CarModel.Where(s => s.BrandID == BrandID && s.ID == CarModelID).Select(x => new
                {
                    ID = x.ID,
                    NazivModela = x.NazivModela
                }).ToList();
            }

            return Json(x);

        }

        // 
        // zakomentarisani dio je prebacen u user controller :)
        /*
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
        public IActionResult Remove(int id)
        {
            var shoppingCart = _db.ShoppingCart.Where(i => i.Id == id).FirstOrDefault();
            _db.ShoppingCart.Remove(shoppingCart);
            _db.SaveChanges();

            return Redirect("/Car/ShoppingCart");
        }
        */
        public IActionResult RemoveCar()
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

            foreach (var item in model)
            {
                var carImageSET = _db.CarImage.Where(x => x.CarID == item.CarId).ToList();
                var AllImages = new List<string>();

                foreach (var slika in carImageSET)
                {
                    var ImageEntity = _db.Image.Where(i => i.ID == slika.ImageID).Select(i => i.PathToImage).FirstOrDefault();
                    AllImages.Add(ImageEntity);
                }
                item.images = AllImages;
            }

            return View(model);
        }

        public IActionResult PregledVozila(int carID)
        {
            ShowCarsVM model = model = _db.Car.Where(x => x.ID == carID).Select(s => new ShowCarsVM
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
            }).FirstOrDefault();


            var carImageSET = _db.CarImage.Where(x => x.CarID == carID).ToList();
            var AllImages = new List<string>();

            foreach (var slika in carImageSET)
            {
                AllImages.Add(_db.Image.Where(x => x.ID == slika.ID).Select(i => i.PathToImage).FirstOrDefault());
            }
            model.images = AllImages;

            return View(model);
        }

        public IActionResult RemoveById(int id)
        {
            List<CarImage> carImgs = _db.CarImage.Where(x => x.CarID == id).ToList();

            List<Image> imgs = new List<Image>();

            foreach (var item in carImgs)
            {
                imgs.Add(_db.Image.Where(x => x.ID == item.ImageID).FirstOrDefault());
            }

            Car car = _db.Car.Find(id);

            foreach (var item in imgs)
            {

                if (System.IO.File.Exists("wwwroot" + item.PathToImage))
                {
                    System.IO.File.Delete("wwwroot" + item.PathToImage);
                }
            }

            FinishedItems finished = new FinishedItems
            {
                Identification = id,
                BrandID = car.BrandID,
                Model = car.Model,
                FuelID = car.FuelID,
                VehicleTypeID = car.VehicleTypeID,
                ColorID = car.ColorID,
                DriveTypeID = car.DriveTypeID,
                TransmissionID = car.TransmissionID,
                NumberOfSeats = car.NumberOfSeats,
                NumberOfDors = car.NumberOfDors,
                NumberOfGears = car.NumberOfGears,
                PowerPS = car.PowerPS,
                PowerKw = car.PowerKw,
                WheelSize = car.WheelSize,
                Ccm = car.Ccm,
                Kilometre = car.Kilometre,
                DateOfManufacture = car.DateOfManufacture,
                DateOfFinish = System.DateTime.Now
            };

            _db.Add(finished);
            _db.RemoveRange(carImgs);
            _db.RemoveRange(imgs);
            _db.Remove(car);

            _db.SaveChanges();

            return Redirect(url: "/Car/RemoveCar");
        }

        private void UcitajSlike(AddCarVM model, int carID)
        {
            List<string> jedinstvenaImenaSlika = new List<string>();

            if (model.Images != null)
            {
                string folder = "wwwroot/Images/";
                bool exists = System.IO.Directory.Exists(folder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(folder);

                foreach (var item in model.Images)
                {
                    string ekstenzija = Path.GetExtension(item.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    item.CopyTo(new FileStream(folder + filename, FileMode.Create));

                    var image = new Image();
                    image.PathToImage = "/Images/" + filename;

                    jedinstvenaImenaSlika.Add(image.PathToImage);

                    _db.Add(image);
                    _db.SaveChanges();

                    _db.Add(new CarImage
                    {
                        CarID = carID,
                        ImageID = image.ID
                    });

                    _db.SaveChanges();
                }
            }
        }
    }
}
