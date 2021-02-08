using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_CS.EF_Models
{
    public class Brand_Model
    {
        public int ID { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int CarModelID { get; set; }
        public CarModel CarModel { get; set; }
    }
}
