using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class ServicedCars
    {
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public int CarID { get; set; }
        public Car CarName { get; set; }
        public int ServicePriceCriteriaID { get; set; }
        public ServicePriceCriteria ServicePriceCriteria { get; set; }
        public DateTime DateOfRegistraion { get; set; }
        public DateTime DateOfServiced { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
