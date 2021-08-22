using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.Service_ang
{
    public class AddCarServiceVM
    {
        public int ID { get; set; } //lista
        public string Warnings { get; set; }
        public string Recommendations { get; set; }
        public string ServiceName { get; set; }
     /*   public int ServicePriceCriteria { get; set; } *///lista
        public DateTime DateofServiceWarranty { get; set; }
        public DateTime DateOfServiced { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SelectedCar { get; set; }
        public List<SelectListItem> Cars { get; set; }
    }
}
