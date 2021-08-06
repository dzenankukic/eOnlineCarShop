using AspNetCore.Reporting;
using Data_CS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TempReportApp;

namespace eOnlineCarShop.Controllers
{
    public class Report1Controller : Controller
    {
        private ApplicationDbContext _db;

        public Report1Controller(ApplicationDbContext db)
        {
            _db = db;
        }

        public static Report1VM getCar(ApplicationDbContext db, int CarId)
        {
            Report1VM podaci = db.Car
                .Where(w=> w.ID == CarId)
                .Select(s => new Report1VM
            {
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

            return podaci;
        }
        public IActionResult Index(int CarId)
        {
            AspNetCore.Reporting.LocalReport _localReport = new AspNetCore.Reporting.LocalReport("Reports/Report1.rdlc");
            Report1VM podaci = getCar(_db, CarId);
            _localReport.AddDataSource("DataSet1", podaci);

            ReportResult result = _localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
    }
}
