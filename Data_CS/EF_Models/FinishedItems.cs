using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class FinishedItems
    {
        public int ID { get; set; }
        public int Identification { get; set; } // stari ID koji je bio prije zavrsavanja vozila
        public int BrandID { get; set; }
        public Brand brand { get; set; }
        public string Model { get; set; }
        public int FuelID { get; set; }
        public Fuel Fuel { get; set; }
        public int VehicleTypeID { get; set; }
        public VehicleType VehicleType { get; set; }
        public int ColorID { get; set; }
        public Color Color { get; set; }
        public int DriveTypeID { get; set; }
        public DriveType DriveType { get; set; }
        public int TransmissionID { get; set; }
        public Transmission Transmission { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDors { get; set; }
        public string NumberOfGears { get; set; }
        public int PowerPS { get; set; }
        public int PowerKw { get; set; }
        public float WheelSize { get; set; }
        public float Ccm { get; set; }
        public float Kilometre { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DateOfFinish { get; set; }
    }
}
